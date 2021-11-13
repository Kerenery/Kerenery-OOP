using System;
using System.ComponentModel;
using Banks.Accounts;
using Banks.Services;
using Banks.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class CreateDebitAccountCommand : Command<CreateDebitAccountCommand.CreateDebitAccountSettings>
    {
        public override int Execute(CommandContext context, CreateDebitAccountSettings settings)
        {
            BanksService banksService = new ();
            banksService.LoadState();
            string[] info = settings.Info.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var clientId = info.Length > 0
                ? new Guid(info[0])
                : throw new BanksException("client id is required param");
            var bankName = info.Length > 1
                ? info[1]
                : throw new BanksException("where is da bank, Lebovski?");
            var balance = info.Length > 2
                ? info[2]
                : throw new BanksException("where is da money, Lebovski?");

            try
            {
                banksService.RegisterAccount(
                    clientId,
                    AccountType.Debit,
                    banksService.FindBank(bankName),
                    new Balance() { WithdrawBalance = Convert.ToDecimal(balance) });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }

            banksService.SaveState();
            AnsiConsole.MarkupLine($"[bold green]Debit account [/] is added successfully!");
            return 0;
        }

        public class CreateDebitAccountSettings : CommandSettings
        {
            [CommandArgument(0, "<account-info>")]
            [Description("Add some info pls")]
            public string Info { get; private set; }
        }
    }
}