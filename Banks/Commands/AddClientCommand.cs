using System;
using System.ComponentModel;
using Banks.Models;
using Banks.Services;
using Banks.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class AddClientCommand : Command<AddClientCommand.AddSettings>
    {
        public override int Execute(CommandContext context, AddSettings settings)
        {
            BanksService banksService = new ();
            banksService.LoadState();
            string[] info = settings.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var client = ClientBuilder.Init()
                .SetName(info.Length > 0 ? info[0] : throw new BanksException("name is required param"))
                .SetSecondName(info.Length > 1 ? info[1] : null)
                .SetAddress(info.Length > 2 ? info[2] : null)
                .SetPassportData(info.Length > 3 ? info[3] : null)
                .Build();

            try
            {
                banksService.RegisterClient(client);
            }
            catch (BanksException exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }

            banksService.SaveState();
            AnsiConsole.MarkupLine($"[bold green]{info[0]}[/] is added!");
            return 0;
        }

        public class AddSettings : CommandSettings
        {
            [CommandArgument(0, "<client-info>")]
            [Description("Add some info pls")]
            public string Name { get; private set; }
        }
    }
}