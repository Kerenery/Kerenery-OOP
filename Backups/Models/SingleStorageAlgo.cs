using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Services;

namespace Backups.Models
{
    public class SingleStorageAlgo : IAlgorithm
    {
        public Storage CreateCopy(RestorePoint restorePoint, IRepository repository, int term)
        {
            var zipToOpen = $@"{repository.Path}\{term}new.zip";

            using (ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update))
            {
                foreach (var jobObjectFile in restorePoint.JobObject.Files)
                {
                    var name = Path.GetFileName(jobObjectFile);
                    archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
                }
            }

            return new Storage() { Path = repository.Path };
        }
    }
}