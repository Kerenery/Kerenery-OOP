using System;
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
        public bool IsMergeable { get; init; }

        public int? PointsLimit { get; init; }

        public RestorePoint MergeClean(Backup backup)
        {
            var directoryInfo = new DirectoryInfo(backup.Repository.Path).GetFiles();

            switch (backup.CreatedBy)
            {
                case AlgoType.SingleStorage:
                    Clean(backup);
                    break;
                case AlgoType.SpliStorage:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Clean(Backup backup)
        {
            if (backup is null)
                throw new BackupsExtraException("backup must be implemented");

            var pointsCount = PointsLimit
                              ?? throw new BackupsExtraException("Points limit is not set");

            var repository = backup.Repository
                              ?? throw new BackupsExtraException("can't find repository u are looking for");

            backup.RemoveRestorePoints(pointsCount);

            var directoryInfo = new DirectoryInfo(repository.Path).GetFiles();
            var filesToDelete = directoryInfo.Where(fi => fi is { })
                .OrderBy(fi => fi.CreationTime)
                .Take(pointsCount)
                .ToList();

            filesToDelete.ForEach(f => f.Delete());
        }
    }
}