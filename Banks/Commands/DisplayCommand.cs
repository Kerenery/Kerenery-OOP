using Banks.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Commands
{
    public class DisplayCommand : Command
    {
        public override int Execute(CommandContext context)
        {
            BanksService banksService = new ();
            banksService.LoadState();
            BanksAndClientsDisplay.DisplayClientsTable(banksService);
            return 0;
        }
    }
}