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
            BackupService backupService = new BackupService();
            Context context = new Context(new SingleStorageAlgo());
            var fabric = new JobFactory(backupService);

            var restorePoint = fabric.CreateRestorePoint(null);
            BackupJob backupJob = BackupJobBuilder
                                    .Init(backupService)
                                    .SetAlgorithm(context)
                                    .SetName("newJob")
                                    .SetRestorePoint(restorePoint)
                                    .Build();

            Console.WriteLine(backupJob.JobName);
        }
    }
}