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
            //     .AutoRefresh(true)
            //     .Spinner(Spinner.Known.Default)
            //     .Start("[yellow]Initializing warp drive[/]", ctx =>
            //     {
            //         // Initialize
            //         Thread.Sleep(3000);
            //         WriteLogMessage("Starting gravimetric field displacement manifold");
            //         Thread.Sleep(1000);
            //         WriteLogMessage("Warming up deuterium chamber");
            //         Thread.Sleep(2000);
            //         WriteLogMessage("Generating antideuterium");
            //
            //         // Warp nacelles
            //         Thread.Sleep(3000);
            //         ctx.Spinner(Spinner.Known.BouncingBar);
            //         ctx.Status("[bold blue]Unfolding warp nacelles[/]");
            //         WriteLogMessage("Unfolding left warp nacelle");
            //         Thread.Sleep(2000);
            //         WriteLogMessage("Left warp nacelle [green]online[/]");
            //         WriteLogMessage("Unfolding right warp nacelle");
            //         Thread.Sleep(1000);
            //         WriteLogMessage("Right warp nacelle [green]online[/]");
            //
            //         // Warp bubble
            //         Thread.Sleep(3000);
            //         ctx.Spinner(Spinner.Known.Star2);
            //         ctx.Status("[bold blue]Generating warp bubble[/]");
            //         Thread.Sleep(3000);
            //         ctx.Spinner(Spinner.Known.Star);
            //         ctx.Status("[bold blue]Stabilizing warp bubble[/]");
            //
            //         // Safety
            //         ctx.Spinner(Spinner.Known.Monkey);
            //         ctx.Status("[bold blue]Performing safety checks[/]");
            //         WriteLogMessage("Enabling interior dampening");
            //         Thread.Sleep(2000);
            //         WriteLogMessage("Interior dampening [green]enabled[/]");
            //
            //         // Warp!
            //         Thread.Sleep(3000);
            //         ctx.Spinner(Spinner.Known.Moon);
            //         WriteLogMessage("Preparing for warp");
            //         Thread.Sleep(1000);
            //         for (var warp = 1; warp < 10; warp++)
            //         {
            //             ctx.Status($"[bold blue]Warp {warp}[/]");
            //             Thread.Sleep(500);
            //         }
            //     });

            // Done
            AnsiConsole.MarkupLine("[bold green]Crusing at Warp 9.8[/]");
        }

        // private static void WriteLogMessage(string message)
        // {
        //     AnsiConsole.MarkupLine($"[grey]LOG:[/] {message}[grey]...[/]");
        // }
    }
}