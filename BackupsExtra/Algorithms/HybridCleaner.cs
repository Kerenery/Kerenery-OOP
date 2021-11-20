using System;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class HybridCleaner : ICleaningAlgorithm
    {
        public bool IsMergeable { get; init; }

        public DateTime? CleaningDate { get; init; }

        public int? PointsLimit { get; init; }

        public Limit? Preference { get; init; }

        public RestorePoint Clean()
        {
            throw new System.NotImplementedException();
        }
    }
}