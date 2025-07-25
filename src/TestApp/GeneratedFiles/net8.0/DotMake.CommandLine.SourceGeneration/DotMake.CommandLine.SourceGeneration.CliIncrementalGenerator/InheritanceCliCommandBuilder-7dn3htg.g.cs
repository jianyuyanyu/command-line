﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v2.6.7.0
// Roslyn (Microsoft.CodeAnalysis) v4.1400.25.27905
// Generation: 1

namespace TestApp.Commands.GeneratedCode
{
    /// <inheritdoc />
    public class InheritanceCliCommandBuilder : DotMake.CommandLine.CliCommandBuilder
    {
        /// <inheritdoc />
        public InheritanceCliCommandBuilder()
        {
            DefinitionType = typeof(TestApp.Commands.InheritanceCliCommand);
            ParentDefinitionType = null;
            ChildDefinitionTypes = null;
            NameAutoGenerate = null;
            NameCasingConvention = null;
            NamePrefixConvention = null;
            ShortFormAutoGenerate = null;
            ShortFormPrefixConvention = null;
        }

        /// <inheritdoc />
        protected override System.CommandLine.Command DoBuild(DotMake.CommandLine.CliBindingContext bindingContext)
        {
            // Command for 'InheritanceCliCommand' class
            var commandName = Namer.GetCommandName("InheritanceCliCommand");
            var command = IsRoot
                ? new System.CommandLine.RootCommand()
                : new System.CommandLine.Command(commandName);
            var rootCommand = command as System.CommandLine.RootCommand;
            Namer.AddShortFormAlias(command, "InheritanceCliCommand");

            // Option for 'Username' property
            var option0Name = Namer.GetOptionName("Username");
            var option0 = new System.CommandLine.Option<string>
            (
                option0Name
            )
            {
                Description = "Username of the identity performing the command",
                Required = false,
                DefaultValueFactory = _ => "admin",
                CustomParser = bindingContext.GetArgumentParser<string>
                (
                    null
                ),
            };
            Namer.AddShortFormAlias(option0, "Username");
            command.Add(option0);

            // Option for 'Password' property
            var option1Name = Namer.GetOptionName("Password");
            var option1 = new System.CommandLine.Option<string>
            (
                option1Name
            )
            {
                Description = "Password of the identity performing the command",
                Required = true,
                CustomParser = bindingContext.GetArgumentParser<string>
                (
                    null
                ),
            };
            Namer.AddShortFormAlias(option1, "Password");
            command.Add(option1);

            // Argument for 'Department' property
            var argument0Name = Namer.GetArgumentName("Department");
            var argument0 = new System.CommandLine.Argument<string>
            (
                argument0Name
            )
            {
                Description = "Department of the identity performing the command (interface)",
                DefaultValueFactory = _ => "Accounting",
                CustomParser = bindingContext.GetArgumentParser<string>
                (
                    null
                ),
            };
            command.Add(argument0);

            bindingContext.CommandMap[command] = DefinitionType;
            bindingContext.CreatorMap[DefinitionType] = () =>
            {
                return new TestApp.Commands.InheritanceCliCommand();
            };
            bindingContext.BinderMap[DefinitionType] = (instance, parseResult) =>
            {
                var definitionInstance = (TestApp.Commands.InheritanceCliCommand)instance;

                // Set the values for the command accessors

                // Set the parsed or default values for the directives

                // Set the parsed or default values for the options
                definitionInstance.Username = bindingContext.GetValue(parseResult, option0);
                definitionInstance.Password = bindingContext.GetValue(parseResult, option1);

                // Set the parsed or default values for the arguments
                definitionInstance.Department = bindingContext.GetValue(parseResult, argument0);
            };

            command.SetAction(parseResult =>
            {
                var definitionInstance = bindingContext.Bind<TestApp.Commands.InheritanceCliCommand>(parseResult);

                // Call the command handler
                var cliContext = new DotMake.CommandLine.CliContext(bindingContext, parseResult);
                var exitCode = 0;
                definitionInstance.Run();

                return exitCode;
            });

            return command;
        }

        [System.Runtime.CompilerServices.ModuleInitializerAttribute]
        internal static void Initialize()
        {
            var commandBuilder = new TestApp.Commands.GeneratedCode.InheritanceCliCommandBuilder();

            // Register this command builder so that it can be found by the definition class
            // and it can be found by the parent definition class if it's a nested/external child.
            commandBuilder.Register();
        }
    }
}
