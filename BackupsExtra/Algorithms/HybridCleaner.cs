using System;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class HybridCleaner : ICleaningAlgorithm
    {
        public bool IsMergeable { get; init; }

        public DateTime? CleaningDate { get; init; }

        public int? PointsLimit { get; init; }

        public Limit? Preference { get; init; }

        public RestorePoint MergeClean(Backup backup)
        {
            throw new System.NotImplementedException();
        }

        void ICleaningAlgorithm.Clean(Backup backup)
        {
            var count = PointsLimit ?? throw new BackupsExtraException("Points count is  not set");
            backup.RemoveRestorePoints(count);
        }
    }
}