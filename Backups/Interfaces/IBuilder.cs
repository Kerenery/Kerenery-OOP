using Backups.Models;

namespace Backups.Interfaces
{
    public interface IBuilder
    {
        Backup BuildBackup(BackupJob backupJob);
        BackupJob BuildBackupJob();
    }
}