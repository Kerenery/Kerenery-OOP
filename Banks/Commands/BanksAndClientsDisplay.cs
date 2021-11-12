using System.Threading;
using Banks.Services;
using Spectre.Console;

namespace Banks.Commands
{
    public static class BanksAndClientsDisplay
    {
        public static void DisplayClientsTable(BanksService banksService)
        {
            var table = new Table().Centered();

            table.AddColumn("Id");
            table.AddColumn("First Name");
            table.AddColumn("Second Name");
            table.AddColumn("Address");
            table.AddColumn("Passport Data").Centered();
            table.Title("[[ [mediumpurple2]List of clients[/] ]]");

            foreach (var client in banksService.GetClients)
            {
                table.AddRow(
                    new Markup($"[underline yellow]{client.Id}[/]"),
                    new Markup($"[underline yellow]{client.Name}[/]"),
                    new Markup($"[underline yellow]{client.SecondName}[/]"),
                    new Markup($"[underline yellow]{client.Address}[/]"),
                    new Markup($"[underline yellow]{client.PassportData}[/]"));
            }

            AnsiConsole.Write(table);

            // AnsiConsole.Status()
            //     .Start("Thinking...", ctx =>
            //     {
            //         // Simulate some work
            //         AnsiConsole.MarkupLine("Doing some work...");
            //         Thread.Sleep(1000);
            //
            //         // Update the status and spinner
            //         ctx.Status("Thinking some more");
            //         ctx.Spinner(Spinner.Known.Star);
            //         ctx.SpinnerStyle(Style.Parse("green"));
            //
            //         // Simulate some work
            //         AnsiConsole.MarkupLine("Doing some more work...");
            //         Thread.Sleep(2000);
            //     });
            AnsiConsole.MarkupLine("[bold green]Crusing at Warp 9.8[/]");
        }
    }
}