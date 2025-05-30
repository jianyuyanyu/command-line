#pragma warning disable CS1591
using DotMake.CommandLine;
using TestApp.Commands.External;

namespace TestApp.Commands
{
    #region RootWithExternalChildrenCliCommand

    /*
        Another way to create hierarchy between commands, especially if you want to use standalone classes,
        is to use `Children` property of `CliCommand` attribute to specify array of `typeof` child classes.
        Command hierarchy in below example is:

         TestApp
         └╴external-level-1
           └╴external-level-2
    */

    [CliCommand(
        Description = "A root cli command with external children",
        Children = new []
        {
            typeof(ExternalLevel1SubCliCommand),
        }
    )]
    public class RootWithExternalChildrenCliCommand
    {
        [CliOption(Description = "Description for Option1")]
        public string Option1 { get; set; } = "DefaultForOption1";

        [CliArgument(Description = "Description for Argument1")]
        public string Argument1 { get; set; } = "DefaultForArgument1";

        public void Run(CliContext context)
        {
            if (context.IsEmptyCommand())
                context.ShowHierarchy();
            else
                context.ShowValues();
        }
    }

    #endregion
}
