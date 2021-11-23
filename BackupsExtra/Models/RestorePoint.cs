using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        private readonly List<string> _files = new ();
        public Guid Id { get; init; }

        public DateTime CreationDate { get; init; }

        public void AddFile(string zipPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(zipPath))
                throw new BackupsExtraException("filepath is empty or whitespace");

            var zipFile = ZipFile.OpenRead(zipPath);
            if (!zipFile.Entries.Any(entry => entry.FullName.EndsWith(fileName)))
                throw new BackupsExtraException($"file is not found, {zipFile} or {fileName} is incorrect");

            zipFile.Dispose();
            if (_files.Any(file => file == fileName))
                throw new BackupsExtraException("file is already added");

            _files.Add(fileName);
        }

        public void AddFile(string zipPath)
        {
            if (string.IsNullOrWhiteSpace(zipPath))
                throw new BackupsExtraException("filepath is empty or whitespace");

            _files.Add(zipPath);
        }

        public List<string> GetFiles() => new (_files);
    }
}