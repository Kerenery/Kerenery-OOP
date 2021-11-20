using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        private List<Backup> _backups = new ();
        private List<BackupJob> _backupJobs = new ();

        public BackupJob AddBackupJob(BackupJob backupJob)
        {
            if (backupJob is null)
            {
                Log.Error("backupJob is null");
                throw new BackupsExtraException("backupJob can't be null");
            }

            if (!Directory.Exists(backupJob.Destination.Path))
                Log.Warning("directory does not exist");

            if (_backups.All(b => b.Repository != backupJob.Destination))
            {
                var backup = new Backup()
                {
                    Repository = backupJob.Destination,
                    Id = Guid.NewGuid(),
                };

                _backups.Add(backup);
                Log.Information($"{backup.Id} backup is created");
            }

            _backupJobs.Add(backupJob);
            var selectedBackup = _backups.First(b => b.Repository == backupJob.Destination);
            selectedBackup.AddRestorePoint(backupJob.Algorithm.Copy(backupJob.JobObject, backupJob.Destination, selectedBackup.Term));
            Log.Information("backupJob process finished, created new restore point");
            return backupJob;
        }

        public Backup FindBackup(Guid id) => _backups.FirstOrDefault(b => b.Id == id);
    }
}