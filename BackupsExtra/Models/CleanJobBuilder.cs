using System;
using BackupsExtra.Interfaces;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Models
{
    public class CleanJobBuilder
    {
        private string _name;
        private Guid _backupId;
        private ICleaningAlgorithm _cleaningAlgorithm;
        private BackupExtraService _backupService;
        private CleanJob _cleanJob;

        private CleanJobBuilder(BackupExtraService backupService)
        {
            _backupService = backupService;
        }

        public static CleanJobBuilder Init(BackupExtraService backupService)
            => new CleanJobBuilder(backupService);

        public CleanJobBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BackupsExtraException("name is empty");

            _name = name;
            return this;
        }

        public CleanJobBuilder SetAlgorithm(ICleaningAlgorithm cleaningAlgorithm)
        {
            _cleaningAlgorithm = cleaningAlgorithm
                                 ?? throw new BackupsExtraException("cleaning algorithm does not exist");
            return this;
        }

        public CleanJobBuilder SetBackupToClean(Guid backupId)
        {
            _backupId = _backupService.FindBackup(backupId) is not null
                ? backupId
                : throw new BackupsExtraException("There is no such backup");

            return this;
        }

        public CleanJob Build()
        {
            if (_name == null || _cleaningAlgorithm == null)
                throw new BackupsExtraException("required fields are not set");

            _cleanJob = new CleanJob()
            {
                Name = _name,
                Id = Guid.NewGuid(),
                CleaningAlgorithm = _cleaningAlgorithm,
                BackupToCleanId = _backupId,
            };

            Log.Information($"Cleaning job {_name} is created successfully");
            _backupService.AddCleanJob(_cleanJob);
            return _cleanJob;
        }
    }
}