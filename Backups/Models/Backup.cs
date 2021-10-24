using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class Backup
    {
        public BackupJob BackupJob { get; private set; }

        public Guid Id { get; init; }

        public BackupJob SetJob(BackupJob backupJob)
        {
            if (BackupJob is null)
                throw new BackupException("Job is already added");

            return BackupJob = backupJob;
        }
    }
}