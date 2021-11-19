using System;
using System.IO;
using BackupsExtra.Interfaces;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Models
{
    public class BackupJobBuilder
    {
        private BackupJob _backupJob;
        private BackupExtraService _backupService;
        private string _name;
        private IAlgorithm _context;
        private RestorePoint _restorePoint;
        private string _filepath;

        private BackupJobBuilder(BackupExtraService backupService)
        {
            _backupService = backupService;
        }

        public static BackupJobBuilder Init(BackupExtraService backupService)
            => new BackupJobBuilder(backupService);

        public BackupJob Build()
        {
            if (_context is null)
                throw new BackupsExtraException("Algorithm can't be null");

            _backupJob = new BackupJob() { Algorithm = _context, Name = _name };
            Log.Information($"backup job with {_name} is created successfully");
            _backupService.AddBackupJob(_backupJob);
            return _backupJob;
        }

        public BackupJobBuilder SetRestorePoint(RestorePoint restorePoint)
        {
            _restorePoint = restorePoint;
            return this;
        }

        public BackupJobBuilder SetAlgorithm(IAlgorithm context)
        {
            _context = context;
            return this;
        }

        public BackupJobBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || _name is not null)
                throw new BackupsExtraException("name can't ve null");

            _name = name;
            return this;
        }

        public BackupJobBuilder ToDestination(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                throw new BackupsExtraException("filepath is empty");

            if (!Directory.Exists(filepath))
            {
                Log.Error("some exception (wip)");
                throw new BackupsExtraException($"{filepath} is not correct, there is no such directory");
            }

            _filepath = filepath;
            return this;
        }
    }
}