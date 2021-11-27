using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using Serilog;

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

            var restorePoints = backup.GetRestorePoints()
                .Where(restorePoint => restorePoint.CreationDate < cleaningDate)
                .ToList();

            var filesToDelete = restorePoints
                .SelectMany(restorePoint => restorePoint.GetFiles(), (_, file) => new FileInfo(file))
                .ToList();

            backup.RemoveRestorePoints(restorePoints.Count);
            filesToDelete.ForEach(f => f.Delete());
            Log.Information("RestorePointDate cleaning is done successfully");
        }
    }
}