using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Enums;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        public Guid Id { get; init; }
        public DateTime CreationDate { get; init; }
        public AlgoType CreatedBy { get; init; }
        public List<string> Files { get; } = new ();

        public void AddFile(string zipPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(zipPath))
                throw new BackupsExtraException("filepath is empty or whitespace");

            var zipFile = ZipFile.OpenRead(zipPath);
            if (!zipFile.Entries.Any(entry => entry.FullName.EndsWith(fileName)))
                throw new BackupsExtraException($"file is not found, {zipFile} or {fileName} is incorrect");

            zipFile.Dispose();
            if (Files.Any(file => file == fileName))
                throw new BadImageFormatException("file is already added");

            Files.Add(fileName);
        }
    }
}