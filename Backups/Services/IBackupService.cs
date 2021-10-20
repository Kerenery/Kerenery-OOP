using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public interface IBackupService
    {
        JobObject CreateJob(List<string> filePaths);
    }
}