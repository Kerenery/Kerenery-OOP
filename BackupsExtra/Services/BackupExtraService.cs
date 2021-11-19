using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Models;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        private List<Backup> _backups = new ();
        private List<BackupJob> _backupJobs = new ();

        public BackupJob AddBackupJob(BackupJob backupJob)
        {
            if(_backups.Any(b => b.Repository == backupJob.))
        }
    }
}