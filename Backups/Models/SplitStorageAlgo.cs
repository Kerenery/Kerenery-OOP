using System.IO;
using System.IO.Compression;
using Backups.Services;

namespace Backups.Models
{
    public class SplitStorageAlgo : IAlgorithm
    {
        public Storage CreateCopy(RestorePoint restorePoint, Storage repository, int term)
        {
            foreach (var jobObjectFile in restorePoint.JobObject.Files)
            {
                var zipToOpen = $@"{repository.Path}{Path.DirectorySeparatorChar}{Path.GetFileName(jobObjectFile)}.zip";
                using ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update);
                var name = Path.GetFileName(jobObjectFile);
                archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
            }

            return new Storage() { Path = repository.Path };
        }
    }
}