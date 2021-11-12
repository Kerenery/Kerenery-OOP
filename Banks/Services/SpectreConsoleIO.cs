﻿using Banks.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.Services
{
    public static class SpectreConsoleIO
    {
        private static readonly CommandApp App = new ();
        public static int Command(string[] args)
        {
            App.Configure(config =>
            {
                config.ValidateExamples();

                config.AddCommand<AddClientCommand>("AddClient")
                    .WithDescription("Create a client")
                    .WithExample(new[] { "AddClient", "Nick Kondratev Puskin 14882281488" });

                config.AddCommand<DisplayCommand>("Display")
                    .WithDescription("Display everything")
                    .WithExample(new[] { "Display" });

                config.AddCommand<CreateCreditAccountCommand>("CreateCreditAccount")
                    .WithDescription("Create a credit account")
                    .WithExample(new[] { "CreateCreditAccount", "1231axzxcasdsfasdfasdf, bankName, money" });
            });

            return App.Run(args);
        }
    }
}