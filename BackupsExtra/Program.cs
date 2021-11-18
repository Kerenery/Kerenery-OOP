using System;
using BackupsExtra.Algorithms;
using BackupsExtra.Enums;
using BackupsExtra.Models;
using Serilog;
using Serilog.Sinks.File;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();

            Log.CloseAndFlush();

            var algoFactory = new AlgorithmFactory();

            var algo = algoFactory.CreateSplitAlgorithm(Limit.RestorePoints, new Repository(), pointsCount: 5);
        }
    }
}
