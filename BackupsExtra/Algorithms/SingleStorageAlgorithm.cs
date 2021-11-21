using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;
using Serilog;

namespace BackupsExtra.Algorithms
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public RestorePoint Copy(JobObject jobObject, Repository repositoryToSave, int term)
        {
            var zipToOpen = Path.Combine(repositoryToSave.Path, $"{Guid.NewGuid().ToString()[..10]}.zip");
            var restorePoint = new RestorePoint()
            {
                Id = Guid.NewGuid(),
                CreatedBy = AlgoType.SingleStorage,
            };

            var files = new List<string>();
            using ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update);

            foreach (var jobObjectFile in jobObject.Files)
            {
                var name = Path.GetFileName(jobObjectFile);
                archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
                files.Add($"{term}_{name}");
            }

            archive.Dispose();

            if (IsFileLocked(new FileInfo(zipToOpen)))
            {
                Log.Warning("file is locked by some process");
                throw new BackupsExtraException("file is locked");
            }

            foreach (var fileName in files)
            {
                restorePoint.AddFile(zipToOpen, fileName);
            }

            Log.Information($"restore point created successfully ");
            return restorePoint;
        }

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}