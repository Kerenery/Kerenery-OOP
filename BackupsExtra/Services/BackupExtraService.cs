using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Algorithms;
using BackupsExtra.Models;
using BackupsExtra.Snapshot;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        private List<Backup> _backups = new ();
        private List<BackupJob> _backupJobs = new ();
        private List<CleanJob> _cleanJobs = new ();

        public BackupJob AddBackupJob(BackupJob backupJob)
        {
            if (backupJob is null)
            {
                Log.Error("backupJob is null");
                throw new BackupsExtraException("backupJob can't be null");
            }

            if (!Directory.Exists(backupJob.Destination.Path))
                Log.Warning("directory does not exist");

            if (_backups.All(b => b.Repository.Path != backupJob.Destination.Path))
            {
                var backup = new Backup()
                {
                    Repository = backupJob.Destination,
                    Id = Guid.NewGuid(),
                    Name = $"backup[{_backups.Count}]",
                };

                _backups.Add(backup);
                Log.Information($"{backup.Id} backup is created");
            }

            _backupJobs.Add(backupJob);
            var selectedBackup = _backups.First(b => b.Repository.Path == backupJob.Destination.Path);
            selectedBackup.AddRestorePoint(backupJob.Algorithm.Copy(backupJob.JobObject, backupJob.Destination, selectedBackup.Term));
            Log.Information("backupJob process finished, created new restore point");
            return backupJob;
        }

        public CleanJob AddCleanJob(CleanJob cleanJob)
        {
            if (cleanJob is null)
            {
                Log.Error("cleanJob is not implemented");
                throw new BackupsExtraException("cleanJob is not implemented");
            }

            if (_backups.All(b => b.Id != cleanJob.BackupToCleanId))
            {
                Log.Error("can't find backup u are looking for");
                throw new BackupsExtraException("can't find backup u are looking for");
            }

            if (_cleanJobs.Any(cj => cj.Id == cleanJob.Id))
            {
                Log.Error("such job is already registered");
                throw new BackupsExtraException("such job is already registered");
            }

            _cleanJobs.Add(cleanJob);
            var selectedBackup = _backups.First(b => b.Id == cleanJob.BackupToCleanId);

            switch (cleanJob.CleaningAlgorithm.IsMergeable)
            {
                case true:
                    selectedBackup.AddRestorePoint(cleanJob.CleaningAlgorithm.MergeClean(selectedBackup));
                    break;
                case false:
                    cleanJob.CleaningAlgorithm.Clean(selectedBackup);
                    break;
            }

            Log.Information("cleaning finished");
            return cleanJob;
        }

        public IShot Save() => new BackupsSnapshot()
        {
            Backups = _backups,
            BackupJobs = _backupJobs,
            CleanJobs = _cleanJobs,
        };

        public void Restore(IShot shot)
        {
            if (shot is not BackupsSnapshot)
                throw new BackupsExtraException($"incorrect shot type - {shot.GetType()}");

            _backups = shot.Backups;
            _backupJobs = shot.BackupJobs;
            _cleanJobs = shot.CleanJobs;
        }

        public Backup FindBackup(Guid id) => _backups.FirstOrDefault(b => b.Id == id);
        public Backup FindBackup(string name) => _backups.FirstOrDefault(b => b.Name == name);
    }
}