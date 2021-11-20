using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Models
{
    public class BackupJob
    {
        public string Name { get; init; }

        public Guid Id { get; init; }

        public DateTime CreationDate { get; init; }

        public IAlgorithm Algorithm { get; init; }

        public Repository Destination { get; init; }

        public JobObject JobObject { get; init; }
    }
}