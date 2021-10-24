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
    }
}