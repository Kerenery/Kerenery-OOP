using Banks.Services;
using Spectre.Console;

namespace Banks.Commands
{
    public class BanksAndClientsDisplay
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

            /*foreach (var client in banksService)
            {
                table.AddRow(
                    new Markup($"[underline yellow]{client.Id}[/]"),
                    new Markup($"[underline yellow]{client.Name}[/]"),
                    new Markup($"[underline yellow]{client.SecondName}[/]"),
                    new Markup($"[underline yellow]{client.Address}[/]"),
                    new Markup($"[underline yellow]{client.PassportData}[/]"));
            }*/

            AnsiConsole.Write(table);
        }
    }
}