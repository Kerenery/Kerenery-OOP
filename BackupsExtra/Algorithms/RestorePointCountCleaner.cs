using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using Serilog;

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

            var filesToDelete = backup.GetRestorePoints()
                .Take(pointsCount)
                .SelectMany(restorePoint => restorePoint.GetFiles(), (_, file) => new FileInfo(file))
                .ToList();

            backup.RemoveRestorePoints(pointsCount);
            filesToDelete.ForEach(f => f.Delete());
            Log.Information("RestorePointCount cleaning is done successfully");
        }
    }
}