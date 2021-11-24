using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class MergeCleaner : ICleaningAlgorithm
    {
        public void Clean(Backup backup)
        {
            var fileNames = new List<string>();
            var filesToDelete = new List<FileInfo>();

            foreach (var file in backup.GetRestorePoints()
                .SelectMany(restorePoint => restorePoint.GetFiles())
                .Reverse())
            {
                using ZipArchive archive = ZipFile.OpenRead(file);
                var zipEntry = archive.Entries.FirstOrDefault()?.Name.Split('_').Last()
                               ?? throw new BackupsExtraException();

                if (fileNames.Any(f => f == zipEntry))
                    filesToDelete.Add(new FileInfo(file));

                fileNames.Add(zipEntry);
                archive.Dispose();
            }

            filesToDelete.ForEach(f => f.Delete());
        }
    }
}