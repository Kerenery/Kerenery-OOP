using System;
using System.IO;
using System.Linq;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Algorithms
{
    public class HybridCleaner : ICleaningAlgorithm
    {
        public DateTime? CleaningDate { get; init; }

        public int? PointsLimit { get; init; }

        public Limit? Preference { get; init; }

        public void Clean(Backup backup)
        {
            if (backup is null)
                throw new BackupsExtraException("backup must be implemented");

            var cleaningDate = CleaningDate
                               ?? throw new BackupsExtraException("Points limit is not set, maybe u need pointsCount cleaner");

            var pointsCount = PointsLimit
                              ?? throw new BackupsExtraException("Points limit is not set, maybe u need pointsDate cleaner");

            var dateLimitRestorePoints = backup.GetRestorePoints()
                .Where(restorePoint => restorePoint.CreationDate < cleaningDate)
                .ToList();

            var pointsCountRestorePoints = backup.GetRestorePoints()
                .OrderBy(rp => rp.CreationDate)
                .Take(pointsCount)
                .ToList();

            var hybridSample = dateLimitRestorePoints.Intersect(pointsCountRestorePoints);
            var filesToDelete = hybridSample
                .SelectMany(restorePoint => restorePoint.GetFiles(), (_, file) => new FileInfo(file))
                .ToList();

            backup.RemoveRestorePoints(hybridSample.Count());
            filesToDelete.ForEach(f => f.Delete());
            Log.Information("hybrid cleaning is done successfully");
        }
    }
}