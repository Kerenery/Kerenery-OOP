using System;
using System.Collections.Generic;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Services
{
    public interface IBackupService
    {
        RestorePoint AddRestorePoint(Guid jobId, RestorePoint restorePoint);
        RestorePoint FindRestorePointByJob(Guid backupJobId);
        void StagedJobObjectRemoveFile(Guid restorePointId, string fileName);
        IRepository InvokeBackup(Guid backupId, Guid backupJobId);
        Backup CreateBackup(string name, Guid backupJobId, string storagePath);
        IRepository BackupAddStorage(Guid backupId, IRepository storage);
    }
}