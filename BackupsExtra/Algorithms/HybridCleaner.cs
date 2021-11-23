using System;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class HybridCleaner : ICleaningAlgorithm
    {
        public DateTime? CleaningDate { get; init; }

        public int? PointsLimit { get; init; }

        public Limit? Preference { get; init; }

        public void Clean(Backup backup) { }
    }
}