﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v1.8.5.0
// Roslyn (Microsoft.CodeAnalysis) v4.900.24.8111
// Generation: 1

namespace GeneratedCode
{
    /// <inheritdoc />
    [DotMake.CommandLine.CliCommandAttribute]
    public class CliCommandAsDelegate_a5kez68 : DotMake.CommandLine.CliCommandAsDelegate
    {
        /// <inheritdoc />
        [DotMake.CommandLine.CliArgumentAttribute]
        public string argument1 { get; set; }

        /// <inheritdoc />
        [DotMake.CommandLine.CliOptionAttribute]
        public bool option1 { get; set; }

        /// <inheritdoc />
        public async System.Threading.Tasks.Task RunAsync()
        {
            await (System.Threading.Tasks.Task)InvokeDelegate
            (
                "a5kez68",
                new object[]
                {
                    argument1, 
                    option1, 
                }
            );
        }

        [System.Runtime.CompilerServices.ModuleInitializerAttribute]
        internal static void Initialize()
        {
            // Register this definition class so that it can be found by the command as delegate hash.
            Register<GeneratedCode.CliCommandAsDelegate_a5kez68>("a5kez68");
        }
    }
}
