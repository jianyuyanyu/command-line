﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v1.8.5.0
// Roslyn (Microsoft.CodeAnalysis) v4.900.24.8111
// Generation: 1

namespace TestApp.Commands.GeneratedCode
{
    /// <inheritdoc />
    public class RecursiveOptionCliCommandBuilder : DotMake.CommandLine.CliCommandBuilder
    {
        /// <inheritdoc />
        public RecursiveOptionCliCommandBuilder()
        {
            DefinitionType = typeof(TestApp.Commands.RecursiveOptionCliCommand);
            ParentDefinitionType = null;
            NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.KebabCase;
            NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.DoubleHyphen;
            ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.SingleHyphen;
            ShortFormAutoGenerate = true;
        }

        private TestApp.Commands.RecursiveOptionCliCommand CreateInstance()
        {
            return new TestApp.Commands.RecursiveOptionCliCommand();
        }

        /// <inheritdoc />
        public override System.CommandLine.CliCommand Build()
        {
            // Command for 'RecursiveOptionCliCommand' class
            var rootCommand = new System.CommandLine.CliRootCommand()
            {
                Description = "A root cli command",
            };

            var defaultClass = CreateInstance();

            // Option for 'RecursiveOption' property
            var option0 = new System.CommandLine.CliOption<bool>
            (
                "--recursive"
            )
            {
                Recursive = true,
                Required = false,
                DefaultValueFactory = _ => defaultClass.RecursiveOption,
                CustomParser = GetArgumentParser<bool>
                (
                    null
                ),
            };
            option0.Aliases.Add("-r");
            rootCommand.Add(option0);

            // Add nested or external registered children
            foreach (var child in Children)
            {
                rootCommand.Add(child.Build());
            }

            BindFunc = (parseResult) =>
            {
                var targetClass = CreateInstance();

                //  Set the parsed or default values for the options
                targetClass.RecursiveOption = GetValueForOption(parseResult, option0);

                //  Set the parsed or default values for the arguments

                return targetClass;
            };

            rootCommand.SetAction(parseResult =>
            {
                var targetClass = (TestApp.Commands.RecursiveOptionCliCommand) BindFunc(parseResult);

                //  Call the command handler
                var cliContext = new DotMake.CommandLine.CliContext(parseResult);
                var exitCode = 0;
                cliContext.ShowHelp();
                return exitCode;
            });

            return rootCommand;
        }

        [System.Runtime.CompilerServices.ModuleInitializerAttribute]
        internal static void Initialize()
        {
            var commandBuilder = new TestApp.Commands.GeneratedCode.RecursiveOptionCliCommandBuilder();

            // Register this command builder so that it can be found by the definition class
            // and it can be found by the parent definition class if it's a nested/external child.
            commandBuilder.Register();
        }

        /// <inheritdoc />
        public class SubCliCommandBuilder : DotMake.CommandLine.CliCommandBuilder
        {
            /// <inheritdoc />
            public SubCliCommandBuilder()
            {
                DefinitionType = typeof(TestApp.Commands.RecursiveOptionCliCommand.SubCliCommand);
                ParentDefinitionType = typeof(TestApp.Commands.RecursiveOptionCliCommand);
                NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.KebabCase;
                NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.DoubleHyphen;
                ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.SingleHyphen;
                ShortFormAutoGenerate = true;
            }

            private TestApp.Commands.RecursiveOptionCliCommand.SubCliCommand CreateInstance()
            {
                return new TestApp.Commands.RecursiveOptionCliCommand.SubCliCommand();
            }

            /// <inheritdoc />
            public override System.CommandLine.CliCommand Build()
            {
                // Command for 'SubCliCommand' class
                var command = new System.CommandLine.CliCommand("sub")
                {
                };

                var defaultClass = CreateInstance();

                // Argument for 'Argument1' property
                var argument0 = new System.CommandLine.CliArgument<string>
                (
                    "argument-1"
                )
                {
                    DefaultValueFactory = _ => defaultClass.Argument1,
                    CustomParser = GetArgumentParser<string>
                    (
                        null
                    ),
                };
                command.Add(argument0);

                // Add nested or external registered children
                foreach (var child in Children)
                {
                    command.Add(child.Build());
                }

                BindFunc = (parseResult) =>
                {
                    var targetClass = CreateInstance();

                    //  Set the parsed or default values for the options

                    //  Set the parsed or default values for the arguments
                    targetClass.Argument1 = GetValueForArgument(parseResult, argument0);

                    return targetClass;
                };

                command.SetAction(async (parseResult, cancellationToken) =>
                {
                    var targetClass = (TestApp.Commands.RecursiveOptionCliCommand.SubCliCommand) BindFunc(parseResult);

                    //  Call the command handler
                    var cliContext = new DotMake.CommandLine.CliContext(parseResult, cancellationToken);
                    var exitCode = 0;
                    await targetClass.RunAsync(cliContext);
                    return exitCode;
                });

                return command;
            }

            [System.Runtime.CompilerServices.ModuleInitializerAttribute]
            internal static void Initialize()
            {
                var commandBuilder = new TestApp.Commands.GeneratedCode.RecursiveOptionCliCommandBuilder.SubCliCommandBuilder();

                // Register this command builder so that it can be found by the definition class
                // and it can be found by the parent definition class if it's a nested/external child.
                commandBuilder.Register();
            }
        }
    }
}
