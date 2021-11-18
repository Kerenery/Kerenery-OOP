using System;
using System.Collections.Generic;
using System.IO;
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
        }
    }
}
