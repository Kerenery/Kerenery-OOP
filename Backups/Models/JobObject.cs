using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        public List<string> Files { get; init; }
        public Guid Id { get; init; }

        public void RemoveFile(string filePath)
        {
            var file = Files.FirstOrDefault(f => f == filePath)
                       ?? throw new BackupException("there is no such file");

            Files.Remove(file);
        }
    }
}