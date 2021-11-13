using System.Threading;
using Banks.Accounts;
using Banks.Services;
using Banks.Tools;
using Spectre.Console;

namespace Banks.Commands
{
    public static class BanksAndClientsDisplay
    {
        public static void DisplayAccounts(BanksService banksService)
        {
            var table = new Table().Centered();

            table.AddColumn("Id");
            table.AddColumn("AccountType");
            table.AddColumn("Holder Name");
            table.AddColumn("Balance");
            table.Title("[[ [mediumpurple2]List of accounts[/] ]]");

            foreach (var account in banksService.GetAccounts())
            {
                Table acc = account switch
                {
                    CreditAccount => table.AddRow(
                        new Markup($"[underline yellow]{account.AccountId}[/]"),
                        new Markup($"[underline yellow]{AccountType.Credit}[/]"),
                        new Markup($"[underline yellow]{account.HolderId}[/]"),
                        new Markup($"[underline yellow]{account.CurrentBalance.WholeBalance}[/]")),
                    DepositAccount => table.AddRow(
                        new Markup($"[underline yellow]{account.AccountId}[/]"),
                        new Markup($"[underline yellow]{AccountType.Deposit}[/]"),
                        new Markup($"[underline yellow]{account.HolderId}[/]"),
                        new Markup($"[underline yellow]{account.CurrentBalance.WholeBalance}[/]")),
                    DebitAccount => table.AddRow(
                        new Markup($"[underline yellow]{account.AccountId}[/]"),
                        new Markup($"[underline yellow]{AccountType.Debit}[/]"),
                        new Markup($"[underline yellow]{account.HolderId}[/]"),
                        new Markup($"[underline yellow]{account.CurrentBalance.WholeBalance}[/]")),
                    _ => throw new BanksException("unknown account type")
                };
            }

            AnsiConsole.Write(table);
        }

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

            AnsiConsole.Status()
                .Start("Thinking...", ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some work...");
                    Thread.Sleep(1000);

                    // Update the status and spinner
                    ctx.Status("Thinking some more");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some more work...");
                    Thread.Sleep(2000);
                });
            AnsiConsole.MarkupLine("[bold green]Crusing at Warp 9.8[/]");
        }
    }
}