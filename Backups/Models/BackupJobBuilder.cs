using System;
using System.Collections.Generic;
using System.Globalization;
using Backups.Services;

namespace Backups.Models
{
    public class BackupJobBuilder
    {
        private readonly BackupJob _backupJob;
        private BackupService _backupService;
        private BackupJobBuilder(BackupService backupService)
        {
            _backupService = backupService;
            _backupJob = new BackupJob() { Id = Guid.NewGuid() };
            _backupService.AddBackupJob(_backupJob);
        }

        public static BackupJobBuilder Init(BackupService backupService)
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

        public BackupJobBuilder SetName(string name)
        {
            _backupJob.SetName(name);
            return this;
        }
    }
}