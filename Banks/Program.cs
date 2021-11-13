using Banks.Services;
using Spectre.Console.Cli;

namespace Banks
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            return SpectreConsoleIO.Command(args);
        }
    }
}
