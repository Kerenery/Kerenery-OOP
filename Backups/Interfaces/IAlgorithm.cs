using System.Collections.Generic;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Services
{
    public interface IAlgorithm
    {
        Storage CreateCopy(RestorePoint restorePoint, Storage repository, int term);
    }
}