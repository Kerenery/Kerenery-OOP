using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class JobFactory
    {
        private IBackupService _backupService;

        public JobFactory(IBackupService backupService)
        {
            _backupService = backupService;
        }

        public JobObject CreateJobObject(string filepath)
        {
            if (!File.Exists(filepath))
                throw new BackupException("such file does not really exist");

            var jobObject = new JobObject() { FilePath = filepath, Id = Guid.NewGuid() };
            return jobObject;
        }

        public RestorePoint CreateRestorePoint(List<JobObject> jobObjects)
        {
            if (jobObjects.Capacity == 0)
                throw new BackupException("there are no jobObjects to save");

            var restorePoint = new RestorePoint(jobObjects)
                { CreationTime = DateTime.Now, Id = Guid.NewGuid() };

            return restorePoint;
        }
    }
}