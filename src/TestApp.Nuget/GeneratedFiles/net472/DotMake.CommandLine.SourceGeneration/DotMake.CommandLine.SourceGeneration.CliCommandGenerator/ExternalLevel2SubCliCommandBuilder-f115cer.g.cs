﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v2.1.0.0
// Roslyn (Microsoft.CodeAnalysis) v4.1300.25.16703
// Generation: 1

namespace TestApp.Commands.External.GeneratedCode
{
    /// <inheritdoc />
    public class ExternalLevel2SubCliCommandBuilder : DotMake.CommandLine.CliCommandBuilder
    {
        /// <inheritdoc />
        public ExternalLevel2SubCliCommandBuilder()
        {
            DefinitionType = typeof(TestApp.Commands.External.ExternalLevel2SubCliCommand);
            ParentDefinitionType = typeof(TestApp.Commands.External.ExternalLevel1SubCliCommand);
            NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.KebabCase;
            NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.DoubleHyphen;
            ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.SingleHyphen;
            ShortFormAutoGenerate = true;
        }

        private TestApp.Commands.External.ExternalLevel2SubCliCommand CreateInstance()
        {
            return new TestApp.Commands.External.ExternalLevel2SubCliCommand();
        }

        /// <inheritdoc />
        public override System.CommandLine.Command Build()
        {
            // Command for 'ExternalLevel2SubCliCommand' class
            var command = new System.CommandLine.Command(GetCommandName("ExternalLevel2"))
            {
                Description = "An external level 2 sub-command",
            };

            var defaultClass = CreateInstance();

            // Option for 'Option1' property
            var option0 = new System.CommandLine.Option<string>
            (
                GetOptionName("Option1")
            )
            {
                Description = "Description for Option1",
                Required = false,
                DefaultValueFactory = _ => defaultClass.Option1,
                CustomParser = GetArgumentParser<string>
                (
                    null
                ),
            };
            AddShortFormAlias(option0);
            command.Add(option0);

            // Argument for 'Argument1' property
            var argument0 = new System.CommandLine.Argument<string>
            (
                GetArgumentName("Argument1")
            )
            {
                Description = "Description for Argument1",
                CustomParser = GetArgumentParser<string>
                (
                    null
                ),
            };
            command.Add(argument0);

            Binder = (parseResult) =>
            {
                var targetClass = CreateInstance();

                //  Set the parsed or default values for the options
                targetClass.Option1 = GetValueForOption(parseResult, option0);

                //  Set the parsed or default values for the arguments
                targetClass.Argument1 = GetValueForArgument(parseResult, argument0);

                //  Set the values for the parent command accessors

                return targetClass;
            };

            command.SetAction(parseResult =>
            {
                var targetClass = (TestApp.Commands.External.ExternalLevel2SubCliCommand) Bind(parseResult);

                //  Call the command handler
                var cliContext = new DotMake.CommandLine.CliContext(parseResult);
                var exitCode = 0;
                targetClass.Run(cliContext);
                return exitCode;
            });

            return command;
        }

        [System.Runtime.CompilerServices.ModuleInitializerAttribute]
        internal static void Initialize()
        {
            var commandBuilder = new TestApp.Commands.External.GeneratedCode.ExternalLevel2SubCliCommandBuilder();

            // Register this command builder so that it can be found by the definition class
            // and it can be found by the parent definition class if it's a nested/external child.
            commandBuilder.Register();
        }
    }
}
