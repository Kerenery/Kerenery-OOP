using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Interfaces;
using Backups.Models;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupService : IBackupService
    {
        private readonly Dictionary<Backup, IRepository> _backups = new ();
        private readonly Dictionary<BackupJob, LinkedList<RestorePoint>> _backupJobs = new ();

        public RestorePoint AddRestorePoint(Guid jobId, RestorePoint restorePoint)
        {
            var backupJob = _backupJobs.Keys.FirstOrDefault(b => b.Id == jobId)
                            ?? throw new BackupException("there is no such backupJob");

            if (_backupJobs[backupJob].Any(rp => rp.Id == restorePoint.Id))
                throw new BackupException("such point is already added");

            _backupJobs[backupJob].AddLast(restorePoint);
            return restorePoint;
        }

        public IRepository BackupAddStorage(Guid backupId, IRepository storage)
        {
            var backup = _backups.Keys.FirstOrDefault(b => b.Id == backupId)
                         ?? throw new BackupException("There is no such backup");

            _backups[backup] = storage;
            return storage;
        }

        public Backup CreateBackup(string name, Guid backupJobId, string storagePath)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(storagePath))
                throw new BackupException($"string is empty {name}");

            if (_backups.Keys.Any(bc => bc.Name == name))
                throw new BackupException("such backup already exists");

            var backup = new Backup() { Name = name };
            _backups.Add(backup, new Storage() { Path = storagePath });
            return backup;
        }

        public BackupJob AddBackupJob(BackupJob backupJob)
        {
            if (_backupJobs.Keys.Any(bc => bc.Id == backupJob.Id))
                throw new BackupException("such backup is already registered");

            _backupJobs.Add(backupJob, new LinkedList<RestorePoint>());

            return backupJob;
        }

        public IRepository InvokeBackup(Guid backupId, Guid backupJobId)
        {
            var backup = _backups.Keys.FirstOrDefault(bc => bc.Id == backupId) ??
                         throw new BackupException("there is no such backup");
            var backupJob = _backupJobs.Keys.FirstOrDefault(bj => bj.Id == backupJobId) ??
                            throw new BackupException("there is no such backup");

            var storage = backupJob.Context
                .CreateCopy(_backupJobs[backupJob].Last(), _backups[backup]);

            return storage;
        }
    }
}