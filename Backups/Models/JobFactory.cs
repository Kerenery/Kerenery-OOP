using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public JobObject CreateJobObject(List<string> filepathes)
        {
            if (!filepathes.Any())
                throw new BackupException($"{filepathes} is empty");

            var jobObject = new JobObject() { Files = filepathes, Id = Guid.NewGuid() };
            return jobObject;
        }

        public RestorePoint CreateRestorePoint(JobObject jobObject)
        {
            if (jobObject.Files.Count == 0)
                throw new BackupException("there are no jobObjects to save");

            var restorePoint = new RestorePoint()
                { JobObject = jobObject, CreationTime = DateTime.Now, Id = Guid.NewGuid() };

            return restorePoint;
        }
    }
}