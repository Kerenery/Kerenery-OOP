using System;
using System.Collections.Generic;
using System.Globalization;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJobBuilder
    {
        private BackupJob _backupJob;
        private BackupService _backupService;
        private string _name;
        private Context _context;
        private RestorePoint _restorePoint;

        private BackupJobBuilder(BackupService backupService)
        {
            _backupService = backupService;
        }

        public static BackupJobBuilder Init(BackupService backupService)
            => new BackupJobBuilder(backupService);

        public BackupJob Build()
        {
            _backupJob = new BackupJob() { Id = Guid.NewGuid(), Context = _context, JobName = _name };
            _backupService.AddBackupJob(_backupJob);
            _backupService.AddRestorePoint(_backupJob.Id, _restorePoint);
            return _backupJob;
        }

        public BackupJobBuilder SetRestorePoint(RestorePoint restorePoint)
        {
            _restorePoint = restorePoint;
            return this;
        }

        public BackupJobBuilder SetAlgorithm(Context context)
        {
            if (!context.IsAlgorithmExists())
                throw new BackupException("algo is not chosen");

            _context = context;
            return this;
        }

        public BackupJobBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || _name is not null)
                throw new BackupException("name of job cant be null or already exists");

            _name = name;
            return this;
        }
    }
}