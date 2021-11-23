using System;
using System.IO;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class RestorePointDateCleaner : ICleaningAlgorithm
    {
        public DateTime? CleaningDate { get; init; }

        public void Clean(Backup backup)
        {
            if (backup is null)
                throw new BackupsExtraException("backup must be implemented");

            var cleaningDate = CleaningDate
                              ?? throw new BackupsExtraException("Points limit is not set");

            var repository = backup.Repository
                             ?? throw new BackupsExtraException("can't find repository u are looking for");

            var directoryInfo = new DirectoryInfo(repository.Path).GetFiles();
            var filesToDelete = directoryInfo.Where(fi => fi is { })
                .OrderBy(fi => fi.CreationTime < cleaningDate)
                .ToList();

            backup.RemoveRestorePoints(filesToDelete.Count);

            filesToDelete.ForEach(f => f.Delete());
        }
    }
}