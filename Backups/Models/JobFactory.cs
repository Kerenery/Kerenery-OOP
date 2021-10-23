using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class JobFactory
    {
        private BackupService _backupService;

        public JobFactory(BackupService backupService)
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

        public BackupJob CreateBackupJob(Context context, LinkedList<RestorePoint> graphPoint)
        {
            if (graphPoint.Count == 0)
                throw new BackupException("there are no restore points");

            if (!context.IsAlgorithmExists())
                throw new BackupException("algorithm is not stated");

            var backupJob = new BackupJob(context, graphPoint);
            return backupJob;
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