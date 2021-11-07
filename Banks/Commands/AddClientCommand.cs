using System;
using System.ComponentModel;
using Banks.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class AddClientCommand : Command<AddClientCommand.AddSettings>
    {
        public override int Execute(CommandContext context, AddSettings settings)
        {
            BanksService banksService = new ();

            // taskRegistry.Load(@"D:\Downloads\book1.json");
            // try { taskRegistry.CreateTask(settings.Name); }
            // catch (ArgumentException e) { AnsiConsole.WriteException(e); return -1; }
            // taskRegistry.Save(@"D:\Downloads\book1.json");
            AnsiConsole.MarkupLine($"The [bold green]{settings.Name}[/] is added!");
            return 0;
        }

        public class AddSettings : CommandSettings
        {
            [CommandArgument(0, "<client-info>")]
            [Description("Add some info pls")]
            public string Name { get; set; }
        }
    }
}