using System;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class RestorePointDateCleaner : ICleaningAlgorithm
    {
        public bool IsMergeable { get; init; }

        public DateTime? CleaningDate { get; init; }

        public RestorePoint MergeClean(Backup backup)
        {
            throw new System.NotImplementedException();
        }

        void ICleaningAlgorithm.Clean(Backup backup)
        {
            throw new NotImplementedException();
        }
    }
}