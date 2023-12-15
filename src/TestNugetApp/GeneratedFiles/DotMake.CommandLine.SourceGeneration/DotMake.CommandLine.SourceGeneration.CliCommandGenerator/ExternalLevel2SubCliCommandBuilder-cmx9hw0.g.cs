﻿// <auto-generated />
// Generated by DotMake.CommandLine.SourceGeneration v1.2.0.0
// Roslyn (Microsoft.CodeAnalysis) v4.800.23.57201
// Generation: 1

namespace TestApp.Commands.External
{
	public class ExternalLevel2SubCliCommandBuilder : DotMake.CommandLine.CommandBuilder
	{
		public ExternalLevel2SubCliCommandBuilder()
		{
			DefinitionType = typeof(TestApp.Commands.External.ExternalLevel2SubCliCommand);
			ParentDefinitionType = typeof(TestApp.Commands.RootWithExternalChildrenCliCommand.Level1SubCliCommand);
			NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.SnakeCase;
			NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.ForwardSlash;
			ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.ForwardSlash;
			ShortFormAutoGenerate = true;
		}

		public override System.CommandLine.Command Build()
		{
			// Command for 'ExternalLevel2SubCliCommand' class
			var command = new System.CommandLine.Command("Level2External")
			{
				Description = "An external level 2 sub-command",
			};
			command.AddAlias("external2Alias");

			var defaultClass = new TestApp.Commands.External.ExternalLevel2SubCliCommand();

			// Option for 'Option1' property
			var option0 = new System.CommandLine.Option<string>("/option_1")
			{
				Description = "Description for Option1",
			};
			option0.SetDefaultValue(defaultClass.Option1);
			option0.AddAlias("/o");
			command.Add(option0);

			// Argument for 'Argument1' property
			var argument0 = new System.CommandLine.Argument<string>("argument_1")
			{
				Description = "Description for Argument1",
			};
			argument0.SetDefaultValue(defaultClass.Argument1);
			command.Add(argument0);

			// Add nested or external registered children
			foreach (var child in Children)
			{
				command.Add(child.Build());
			}

			System.CommandLine.Handler.SetHandler(command, context =>
			{
				var targetClass = new TestApp.Commands.External.ExternalLevel2SubCliCommand();

				//  Set the parsed or default values for the options
				targetClass.Option1 = context.ParseResult.GetValueForOption(option0);

				//  Set the parsed or default values for the arguments
				targetClass.Argument1 = context.ParseResult.GetValueForArgument(argument0);

				//  Call the command handler
				targetClass.Run();
			});

			return command;
		}

		[System.Runtime.CompilerServices.ModuleInitializerAttribute]
		public static void Initialize()
		{
			var commandBuilder = new TestApp.Commands.External.ExternalLevel2SubCliCommandBuilder();

			// Register this command builder so that it can be found by the definition class
			// and it can be found by the parent definition class if it's a nested/external child.
			commandBuilder.Register();
		}

		public class Level3SubCliCommandBuilder : DotMake.CommandLine.CommandBuilder
		{
			public Level3SubCliCommandBuilder()
			{
				DefinitionType = typeof(TestApp.Commands.External.ExternalLevel2SubCliCommand.Level3SubCliCommand);
				ParentDefinitionType = typeof(TestApp.Commands.External.ExternalLevel2SubCliCommand);
				NameCasingConvention = DotMake.CommandLine.CliNameCasingConvention.SnakeCase;
				NamePrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.ForwardSlash;
				ShortFormPrefixConvention = DotMake.CommandLine.CliNamePrefixConvention.ForwardSlash;
				ShortFormAutoGenerate = true;
			}

			public override System.CommandLine.Command Build()
			{
				// Command for 'Level3SubCliCommand' class
				var command = new System.CommandLine.Command("level_3")
				{
					Description = "A nested level 3 sub-command",
				};

				var defaultClass = new TestApp.Commands.External.ExternalLevel2SubCliCommand.Level3SubCliCommand();

				// Option for 'Option1' property
				var option0 = new System.CommandLine.Option<string>("/option_1")
				{
					Description = "Description for Option1",
				};
				option0.SetDefaultValue(defaultClass.Option1);
				option0.AddAlias("/o");
				command.Add(option0);

				// Argument for 'Argument1' property
				var argument0 = new System.CommandLine.Argument<string>("argument_1")
				{
					Description = "Description for Argument1",
				};
				argument0.SetDefaultValue(defaultClass.Argument1);
				command.Add(argument0);

				// Add nested or external registered children
				foreach (var child in Children)
				{
					command.Add(child.Build());
				}

				System.CommandLine.Handler.SetHandler(command, context =>
				{
					var targetClass = new TestApp.Commands.External.ExternalLevel2SubCliCommand.Level3SubCliCommand();

					//  Set the parsed or default values for the options
					targetClass.Option1 = context.ParseResult.GetValueForOption(option0);

					//  Set the parsed or default values for the arguments
					targetClass.Argument1 = context.ParseResult.GetValueForArgument(argument0);

					//  Call the command handler
					targetClass.Run();
				});

				return command;
			}

			[System.Runtime.CompilerServices.ModuleInitializerAttribute]
			public static void Initialize()
			{
				var commandBuilder = new TestApp.Commands.External.ExternalLevel2SubCliCommandBuilder.Level3SubCliCommandBuilder();

				// Register this command builder so that it can be found by the definition class
				// and it can be found by the parent definition class if it's a nested/external child.
				commandBuilder.Register();
			}
		}
	}
}
