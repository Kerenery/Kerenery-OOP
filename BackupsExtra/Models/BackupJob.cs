using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Models
{
    public class BackupJob
    {
        public string Name { get; init; }

        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreationDate { get; } = DateTime.Now;

        public IAlgorithm Algorithm { get; init; }

        public Repository Destination { get; init; }
    }
}