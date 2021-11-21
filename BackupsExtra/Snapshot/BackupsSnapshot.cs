using System.Collections.Generic;
using BackupsExtra.Models;

namespace BackupsExtra.Snapshot
{
    public class BackupsSnapshot : IShot
    {
        public List<Backup> Backups { get; init; }
        public List<BackupJob> BackupJobs { get; init; }
        public List<CleanJob> CleanJobs { get; init; }
    }
}