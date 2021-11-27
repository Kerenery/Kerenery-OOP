using System;
using System.Collections.Generic;
using System.IO;
using BackupsExtra.Algorithms;
using BackupsExtra.Enums;
using BackupsExtra.Models;
using BackupsExtra.Services;
using BackupsExtra.Snapshot;
using Newtonsoft.Json;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            var backupService = new BackupExtraService();
            var backupKeeper = new Keeper(backupService);

            backupKeeper.Restore();
        }
    }
}
