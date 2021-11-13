using System;
using Banks.Services;
using Banks.Tools;
using Spectre.Cli;
using Spectre.Console;

namespace Banks.Commands
{
    public class WithdrawCommand : Command<WithdrawCommand.WithdrawCommandSettings>
    {
        public override int Execute(CommandContext context, WithdrawCommandSettings settings)
        {
            BanksService banksService = new ();
            banksService.LoadState();
            string[] info = settings.Info.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var account = info.Length > 0
                ? new Guid(info[0])
                : throw new BanksException("client id is required param");
            var money = info.Length > 1
                ? info[1]
                : throw new BanksException("where is da bank, Lebovski?");

            try
            {
                banksService.Withdraw(banksService.FindAccount(account), Convert.ToDecimal(money));
            }
            catch (BanksException exception)
            {
                AnsiConsole.WriteException(exception);
                return -1;
            }

            banksService.SaveState();
            AnsiConsole.MarkupLine($"[bold green]withdraw baby[/]");
            return 0;
        }

        public class WithdrawCommandSettings : CommandSettings
        {
            [CommandArgument(0, "<withdraw-info>")]
            public string Info { get; private set; }
        }
    }
}