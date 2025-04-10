using System.Collections.Generic;
using System.Linq;
using DotMake.CommandLine.SourceGeneration.Inputs;
using DotMake.CommandLine.SourceGeneration.Util;
using Microsoft.CodeAnalysis.CSharp;

namespace DotMake.CommandLine.SourceGeneration.Outputs
{
    public class CliOptionOutput : OutputBase
    {
        public const string OptionClassName = "Option";
        public const string OptionClassNamespace = "System.CommandLine";

        public static readonly string[] Suffixes = CliCommandOutput.Suffixes
            .Select(s => s + "Option")
            .Append("Option")
            .ToArray();
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

        public new CliOptionInput Input { get; set; }

        public void AppendCSharpCreateString(CodeStringBuilder sb, string varName, string varDefaultValue)
        {
            var optionName = Input.AttributeArguments.TryGetValue(nameof(CliOptionAttribute.Name), out var nameValue)
                             && !string.IsNullOrWhiteSpace(nameValue.ToString())
                ? $"\"{nameValue.ToString().Trim()}\""
                : $"GetOptionName(\"{Input.Symbol.Name.StripSuffixes(Suffixes)}\")";

            sb.AppendLine($"// Option for '{Input.Symbol.Name}' property");
            using (sb.AppendParamsBlockStart($"var {varName} = new {OptionClassNamespace}.{OptionClassName}<{Input.Symbol.Type.ToReferenceString()}>"))
            {
                sb.AppendLine($"{optionName}");
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
                    sb.AppendLine($"DefaultValueFactory = _ => {varDefaultValue},");

                var argumentParserOutput = new CliArgumentParserOutput(Input.ArgumentParser);
                argumentParserOutput.AppendCSharpCallString(sb, "CustomParser", ",");
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

            sb.AppendLine($"AddShortFormAlias({varName});");

            if (Input.AttributeArguments.TryGetValues(nameof(CliOptionAttribute.Aliases), out var aliasesValues))
            {
                foreach (var aliasValue in aliasesValues)
                {
                    if (aliasValue == null)
                        continue;

                    var alias = aliasValue.ToString().Trim();

                    sb.AppendLine($"AddAlias({varName}, \"{alias}\");");
                }
            }
        }
    }
}
