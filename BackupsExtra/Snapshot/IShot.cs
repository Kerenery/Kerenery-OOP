using System.Collections.Generic;
using BackupsExtra.Models;

namespace BackupsExtra.Snapshot
{
    public interface IShot
    {
        public List<Backup> Backups { get; }
        public List<BackupJob> BackupJobs { get; }
        public List<CleanJob> CleanJobs { get; }
    }
}