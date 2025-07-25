using System.Collections.Generic;
using System.Linq;
using DotMake.CommandLine.SourceGeneration.Inputs;
using DotMake.CommandLine.SourceGeneration.Util;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotMake.CommandLine.SourceGeneration.Outputs
{
    public class CliOptionOutput : OutputBase
    {
        public const string OptionClassName = "Option";
        public const string OptionClassNamespace = "System.CommandLine";

        public static readonly Dictionary<string, string> PropertyMappings = new()
        {
            //{ nameof(CliOptionAttribute.HelpName), "ArgumentHelpName"},
            //{ nameof(CliOptionAttribute.Hidden), "IsHidden"}
        };

        public CliOptionOutput(CliOptionInput input)
            : base(input)
        {
            Input = input;
        }

        public new CliOptionInput Input { get; }

        public void AppendCSharpCreateString(CodeStringBuilder sb, string varName, string varNamer, string varBindingContext)
        {
            var varNameParameter = $"{varName}Name";

            sb.AppendLine($"// Option for '{Input.Symbol.Name}' property");

            if (Input.AttributeArguments.TryGetValue(nameof(CliOptionAttribute.Name), out var nameValue))
                sb.AppendLine($"var {varNameParameter} = {varNamer}.GetOptionName(\"{Input.Symbol.Name}\", \"{nameValue}\");");
            else
                sb.AppendLine($"var {varNameParameter} = {varNamer}.GetOptionName(\"{Input.Symbol.Name}\");");

            using (sb.AppendParamsBlockStart($"var {varName} = new {OptionClassNamespace}.{OptionClassName}<{Input.Symbol.Type.ToReferenceString()}>"))
            {
                sb.AppendLine($"{varNameParameter}");
            }
            using (sb.AppendBlockStart(null, ";"))
            {
                foreach (var kvp in Input.AttributeArguments)
                {
                    switch (kvp.Key)
                    {
                        case nameof(CliOptionAttribute.Description):
                        case nameof(CliOptionAttribute.HelpName):
                        case nameof(CliOptionAttribute.Hidden):
                        case nameof(CliOptionAttribute.Recursive):
                        case nameof(CliOptionAttribute.AllowMultipleArgumentsPerToken):
                            if (!PropertyMappings.TryGetValue(kvp.Key, out var propertyName))
                                propertyName = kvp.Key;

                            if (Input.AttributeArguments.TryGetResourceProperty(kvp.Key, out var resourceProperty))
                                sb.AppendLine($"{propertyName} = {resourceProperty.ToReferenceString()},");
                            else
                                sb.AppendLine($"{propertyName} = {kvp.Value.ToCSharpString()},");
                            break;
                        case nameof(CliOptionAttribute.Arity):
                            var arity = kvp.Value.ToCSharpString().Split('.').Last();
                            sb.AppendLine($"{kvp.Key} = {CliArgumentOutput.ArgumentClassNamespace}.{CliArgumentOutput.ArgumentArityClassName}.{arity},");
                            break;
                    }
                }

                //Required is special as it can be calculated when CliOptionAttribute.Required is missing (not forced)
                sb.AppendLine($"Required = {Input.Required.ToString().ToLowerInvariant()},");
                if (!Input.Required)
                {
                    //No more using a default instance here to avoid IServiceProvider integration causing unnecessary instantiations.
                    //Instead, we read the property initializer SyntaxNode, qualify symbols and then use that SyntaxNode for DefaultValueFactory.
                    //However, we still need an uninitialized instance for being able to call AddCompletions method, for now.
                    //The uninitialized instance can not be used for property access, as the constructor is skipped, properties will come as null.
                    //sb.AppendLine($"DefaultValueFactory = _ => {varDefaultClass}.{Input.Symbol.Name},");

                    SyntaxNode valueExpression = null;
                    if (Input.SyntaxNode is PropertyDeclarationSyntax propertyDeclarationSyntax
                        && propertyDeclarationSyntax.Initializer != null)
                    {
                        valueExpression = new QualifiedSyntaxRewriter(Input.SemanticModel)
                            .Visit(propertyDeclarationSyntax.Initializer.Value);

                    }

                    if (valueExpression != null)
                        sb.AppendLine($"DefaultValueFactory = _ => {valueExpression},");
                }

                var argumentParserOutput = new CliArgumentParserOutput(Input.ArgumentParser);
                argumentParserOutput.AppendCSharpCallString(sb, "CustomParser", varBindingContext, ",");
            }

            if (Input.AttributeArguments.TryGetTypedConstant(nameof(CliOptionAttribute.AllowedValues), out var allowedValuesTypedConstant))
                sb.AppendLine($"{varName}.AcceptOnlyFromAmong(new[] {allowedValuesTypedConstant.ToCSharpString()});");

            if (Input.AttributeArguments.TryGetTypedConstant(nameof(CliOptionAttribute.ValidationRules), out var validationRulesTypedConstant))
                sb.AppendLine($"DotMake.CommandLine.CliValidationExtensions.AddValidator({varName}, {validationRulesTypedConstant.ToCSharpString()});");

            if (Input.AttributeArguments.TryGetTypedConstant(nameof(CliOptionAttribute.ValidationPattern), out var validationPatternTypedConstant))
            {
                if (Input.AttributeArguments.TryGetTypedConstant(nameof(CliOptionAttribute.ValidationMessage), out var validationMessageTypedConstant))
                    sb.AppendLine($"DotMake.CommandLine.CliValidationExtensions.AddValidator({varName}, {validationPatternTypedConstant.ToCSharpString()}, {validationMessageTypedConstant.ToCSharpString()});");
                else
                    sb.AppendLine($"DotMake.CommandLine.CliValidationExtensions.AddValidator({varName}, {validationPatternTypedConstant.ToCSharpString()});");
            }

            if (Input.AttributeArguments.TryGetValue(nameof(CliOptionAttribute.Alias), out var aliasValue))
                sb.AppendLine($"{varNamer}.AddShortFormAlias({varName}, \"{Input.Symbol.Name}\", \"{aliasValue}\");");
            else
                sb.AppendLine($"{varNamer}.AddShortFormAlias({varName}, \"{Input.Symbol.Name}\");");

            if (Input.AttributeArguments.TryGetValues(nameof(CliOptionAttribute.Aliases), out var aliasesValues))
            {
                foreach (string alias in aliasesValues)
                    sb.AppendLine($"{varNamer}.AddAlias({varName}, \"{Input.Symbol.Name}\", \"{alias}\");");
            }

            if (Input.Parent.HasGetCompletionsInterface)
                //sb.AppendLine($"{varDefaultClass}.AddCompletions(\"{Input.Symbol.Name}\", {varName}.CompletionSources);");
                sb.AppendLine($"{varName}.CompletionSources.Add(completionContext => GetCompletions(\"{Input.Symbol.Name}\", {varBindingContext}, completionContext));");
        }
    }
}
