using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
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
            var fabric = new JobFactory(backupService);

            // var restorePoint = fabric.CreateRestorePoint(null);
            BackupJob backupJob = BackupJobBuilder
                                    .Init(backupService)
                                    .SetAlgorithm(context)

                                   // .SetRestorePoint(restorePoint)
                                    .Build();

            Console.WriteLine(backupJob.Context.IsAlgorithmExists());
        }
    }
}