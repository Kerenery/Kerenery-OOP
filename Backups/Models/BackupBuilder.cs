using Backups.Interfaces;

namespace Backups.Models
{
    public class BackupBuilder
    {
        private readonly Backup _backup = new Backup();
        private IRepository _storage;

        private BackupBuilder(IRepository storage)
        {
            _storage = storage;
        }

        public static BackupBuilder Init(Storage storage)
            => new BackupBuilder(storage);

        public Backup Build() => _backup;

        public BackupBuilder SetBackupJob(BackupJob backupJob)
        {
            _backup.SetJob(backupJob);
            return this;
        }

        public BackupJob SetStorageDirectory(int id)
        {
        }
    }
}