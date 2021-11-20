using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Models
{
    public class CleanJob
    {
        public Guid BackupToCleanId { get; init; }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public ICleaningAlgorithm CleaningAlgorithm { get; init; }
    }
}