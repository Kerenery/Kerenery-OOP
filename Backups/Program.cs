using System;
using System.Collections.Generic;
using Backups.Models;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            IBackupService backupService = new BackupService();
            Context context = new Context(new SingleStorageAlgo());
            BackupJob backupJob = BackupJobBuilder
                                    .Init(context, backupService)
                                    .Build();
        }
    }
}