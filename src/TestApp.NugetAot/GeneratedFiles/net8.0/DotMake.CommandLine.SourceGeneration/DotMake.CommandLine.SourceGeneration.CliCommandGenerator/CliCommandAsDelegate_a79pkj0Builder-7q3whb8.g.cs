﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v2.0.0.0
// Roslyn (Microsoft.CodeAnalysis) v4.1300.25.16703
// Generation: 1

namespace GeneratedCode
{
    /// <inheritdoc />
    public class CliCommandAsDelegate_a79pkj0Builder : DotMake.CommandLine.CliCommandBuilder
    {
        /// <inheritdoc />
        public CliCommandAsDelegate_a79pkj0Builder()
        {
            DefinitionType = typeof(GeneratedCode.CliCommandAsDelegate_a79pkj0);
            ParentDefinitionType = null;
            NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.KebabCase;
            NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.DoubleHyphen;
            ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.SingleHyphen;
            ShortFormAutoGenerate = true;
        }

        private GeneratedCode.CliCommandAsDelegate_a79pkj0 CreateInstance()
        {
            return new GeneratedCode.CliCommandAsDelegate_a79pkj0();
        }

        /// <inheritdoc />
        public override System.CommandLine.Command Build()
        {
            // Command for 'CliCommandAsDelegate_a79pkj0' class
            var rootCommand = new System.CommandLine.RootCommand()
            {
            };

            var defaultClass = CreateInstance();

            // Option for 'option1' property
            var option0 = new System.CommandLine.Option<bool>
            (
                "--option-1"
            )
            {
                Required = false,
                DefaultValueFactory = _ => defaultClass.option1,
                CustomParser = GetArgumentParser<bool>
                (
                    null
                ),
            };
            option0.Aliases.Add("-o");
            rootCommand.Add(option0);

            // Argument for 'argument1' property
            var argument0 = new System.CommandLine.Argument<string>
            (
                "argument-1"
            )
            {
                CustomParser = GetArgumentParser<string>
                (
                    null
                ),
            };
            rootCommand.Add(argument0);

            Binder = (parseResult) =>
            {
                var targetClass = CreateInstance();

                //  Set the parsed or default values for the options
                targetClass.option1 = GetValueForOption(parseResult, option0);

                //  Set the parsed or default values for the arguments
                targetClass.argument1 = GetValueForArgument(parseResult, argument0);

                //  Set the values for the parent command accessors

                return targetClass;
            };

            rootCommand.SetAction(async (parseResult, cancellationToken) =>
            {
                var targetClass = (GeneratedCode.CliCommandAsDelegate_a79pkj0) Bind(parseResult);

                //  Call the command handler
                var cliContext = new DotMake.CommandLine.CliContext(parseResult, cancellationToken);
                var exitCode = 0;
                exitCode = await targetClass.RunAsync();
                return exitCode;
            });

            return rootCommand;
        }

        [System.Runtime.CompilerServices.ModuleInitializerAttribute]
        internal static void Initialize()
        {
            var commandBuilder = new GeneratedCode.CliCommandAsDelegate_a79pkj0Builder();

            // Register this command builder so that it can be found by the definition class
            // and it can be found by the parent definition class if it's a nested/external child.
            commandBuilder.Register();
        }
    }
}
