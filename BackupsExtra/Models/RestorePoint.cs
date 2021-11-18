using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.Now;
        public List<string> Files { get; } = new ();

        public void AddFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new BackupsExtraException("filepath is empty or whitespace");

            if (!File.Exists(filePath))
                throw new BackupsExtraException($"cannot find file on {filePath}");

            if (Files.Any(file => file == filePath))
                throw new BadImageFormatException("file is already added");

            Files.Add(filePath);
        }
    }
}