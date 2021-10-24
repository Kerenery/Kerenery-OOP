using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Models
{
    public class BackupJobBuilder
    {
        private readonly BackupJob _backupJob;
        private IBackupService _backupService;
        private BackupJobBuilder(Context context, IBackupService backupService)
        {
            _backupService = backupService;
            _backupJob = new BackupJob() { Context = context, Id = Guid.NewGuid() };
        }

        public static BackupJobBuilder Init(Context context, IBackupService backupService)
            => new BackupJobBuilder(context, backupService);

        public BackupJob Build() => _backupJob;

        public BackupJobBuilder SetRestorePoint(RestorePoint restorePoint)
        {
            _backupService.AddRestorePoint(_backupJob.Id, restorePoint);
            return this;
        }
    }
}