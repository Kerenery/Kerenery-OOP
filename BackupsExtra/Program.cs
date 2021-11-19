using System;
using System.Collections.Generic;
using System.IO;
using BackupsExtra.Algorithms;
using BackupsExtra.Enums;
using BackupsExtra.Models;
using Serilog;
using Serilog.Formatting.Json;
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
                .WriteTo.File(new JsonFormatter(), Path.Combine(Directory.GetCurrentDirectory(), "log.json"))
                .CreateLogger();

            Log.CloseAndFlush();
        }
    }
}
