using System.Collections.Generic;
using Backups.Models;

namespace Backups.Services
{
    public interface IAlgorithm
    {
        RestorePoint CreateCopy(JobObject job);
    }
}