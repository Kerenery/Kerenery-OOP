using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupService : IBackupService
    {
        private readonly List<Backup> _backups = new List<Backup>();

        public BackupJob AddRestorePoint(Guid backupId, RestorePoint restorePoint)
        {
            var backup = _backups.FirstOrDefault(b => b.Id == backupId)
                         ?? throw new BackupException("there is no such backup");
            
            backup.
        }

        public BackupJob JobObjectRemoveFile(BackupJob job, string fileName)
        {
            throw new System.NotImplementedException();
        }

        public BackupJob CreateJob(List<string> filePaths, string repositoryPath)
        {
            throw new System.NotImplementedException();
        }

        public JobObject AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
            return jobObject;
        }
    }
}