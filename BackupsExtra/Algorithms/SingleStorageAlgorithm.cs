using System;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public RestorePoint Copy(JobObject jobObject, Repository repositoryToSave, int term)
        {
            var zipToOpen = Path.Combine(repositoryToSave.Path, "bebra.zip");
            var restorePoint = new RestorePoint();
            using (ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update))
            {
                foreach (var jobObjectFile in jobObject.Files)
                {
                    var name = Path.GetFileName(jobObjectFile);
                    archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
                }
            }

            return restorePoint;
        }
    }
}