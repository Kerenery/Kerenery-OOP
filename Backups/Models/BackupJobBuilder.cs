using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Models
{
    public class BackupJobBuilder
    {
        private readonly BackupJob _backupJob;
        private IBackupService _backupService;
        private BackupJobBuilder(IBackupService backupService)
        {
            _backupService = backupService;
            _backupJob = new BackupJob() { Id = Guid.NewGuid() };
        }

        public static BackupJobBuilder Init(IBackupService backupService)
            => new BackupJobBuilder(backupService);

        public BackupJob Build() => _backupJob;

        public BackupJobBuilder SetRestorePoint(RestorePoint restorePoint)
        {
            _backupService.AddRestorePoint(_backupJob.Id, restorePoint);
            return this;
        }

        public BackupJobBuilder SetAlgorithm(Context context)
        {
            _backupJob.SetAlgorithm(context);
            return this;
        }
    }
}