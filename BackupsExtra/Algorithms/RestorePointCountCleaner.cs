using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class RestorePointCountCleaner : ICleaningAlgorithm
    {
        public int? PointsLimit { get; init; }

        public void Clean(Backup backup)
        {
            if (backup is null)
                throw new BackupsExtraException("backup must be implemented");

            var pointsCount = PointsLimit
                              ?? throw new BackupsExtraException("Points limit is not set");

            var repository = backup.Repository
                              ?? throw new BackupsExtraException("can't find repository u are looking for");

            var directoryInfo = new DirectoryInfo(repository.Path).GetFiles();

            List<FileInfo> filesToDelete = new ();

            switch (backup.CreatedBy)
            {
                case AlgoType.SingleStorage:
                    filesToDelete = directoryInfo.Where(fi => fi is { })
                        .OrderBy(fi => fi.CreationTime)
                        .Take(pointsCount)
                        .ToList();
                    break;
                case AlgoType.SplitStorage:
                    filesToDelete = backup.GetRestorePoints()
                        .SelectMany(restorePoint => restorePoint.GetFiles(), (_, file) => new FileInfo(file))
                        .Take(pointsCount)
                        .ToList();
                    break;
                default:
                    throw new BackupsExtraException("unknown algo type");
            }

            backup.RemoveRestorePoints(pointsCount);
            filesToDelete.ForEach(f => f.Delete());
        }
    }
}