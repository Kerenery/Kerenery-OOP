using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Validation;

namespace Backups.Models
{
    public class Backup
    {
        private LinkedList<RestorePoint> _graphPoint;
        public Guid Id { get; init; }

        public BackupJob AddBackupJob(BackupJob backupJob)
        {
            backupJob.GetNodes.PerformOperation(this, backupJob);
        }
    }
}