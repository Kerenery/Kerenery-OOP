using System;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class RestorePointDateCleaner : ICleaningAlgorithm
    {
        public bool IsMergeable { get; init; }

        public DateTime? CleaningDate { get; init; }

        public RestorePoint Clean()
        {
            throw new System.NotImplementedException();
        }
    }
}