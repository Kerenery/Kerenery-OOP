using System;
using Banks.Services;
using Banks.Tools;
using Spectre.Cli;
using Spectre.Console;

namespace Banks.Commands
{
    public class TransferCommand : Command<TransferCommand.TransferCommandSettings>
    {
        public override int Execute(CommandContext context, TransferCommandSettings settings)
        {
            BanksService banksService = new ();
            banksService.LoadState();
            string[] info = settings.Info.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var money = info.Length > 0
                ? info[0]
                : throw new BanksException("money is required param");
            var senderId = info.Length > 1
                ? new Guid(info[1])
                : throw new BanksException("sender is poop");
            var receiverId = info.Length > 2
                ? new Guid(info[2])
                : throw new BanksException("receiver is poop");

            try
            {
                banksService.Transfer(Convert.ToDecimal(money), senderId, receiverId);
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

        public class TransferCommandSettings : CommandSettings
        {
            [CommandArgument(0, "<withdraw-info>")]
            public string Info { get; private set; }
        }
    }
}