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
        private JobObject _jobObject;
        private string _filepath;

        private BackupJobBuilder(BackupExtraService backupService)
        {
            _backupService = backupService;
        }

        public static BackupJobBuilder Init(BackupExtraService backupService)
            => new BackupJobBuilder(backupService);

        public BackupJob Build()
        {
            if (_context is null || _jobObject is null || _filepath is null)
                throw new BackupsExtraException("required fields can't be null");

            _backupJob = new BackupJob()
            {
                Algorithm = _context,
                Name = _name,
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Destination = new Repository() { Path = _filepath },
                JobObject = _jobObject,
            };

            Log.Information($"backup job {_name} is created successfully");
            _backupService.AddBackupJob(_backupJob);
            return _backupJob;
        }

        public BackupJobBuilder SetJobObject(JobObject jobObject)
        {
            if (jobObject.Files.Count == 0)
                throw new BackupsExtraException("job object is empty");

            _jobObject = jobObject;
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
                throw new BackupsExtraException($"{filepath} is not correct, there is no such directory");
            }

            _filepath = filepath;
            return this;
        }
    }
}