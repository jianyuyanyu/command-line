using DotMake.CommandLine;
using System;

// ReSharper disable once CheckNamespace
[CliCommand(Description = "A cli command with class in global namespace")]
public class GlobalNamespaceCliCommand
{
    [CliOption(Description = "Description for Option1")]
    public string Option1 { get; set; } = "DefaultForOption1";

    [CliArgument(Description = "Description for Argument1")]
    public string Argument1 { get; set; } = "DefaultForArgument1";

    public void Run()
    {
        Console.WriteLine($@"Handler for '{GetType().FullName}' is run:");
        Console.WriteLine($@"Value for {nameof(Option1)} property is '{Option1}'");
        Console.WriteLine($@"Value for {nameof(Argument1)} property is '{Argument1}'");
        Console.WriteLine();
    }
}
