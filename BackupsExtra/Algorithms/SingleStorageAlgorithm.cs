using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using Serilog;

namespace BackupsExtra.Algorithms
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public RestorePoint Copy(JobObject jobObject, Repository repositoryToSave, int term)
        {
            var zipToOpen = Path.Combine(repositoryToSave.Path, $"{Guid.NewGuid().ToString()[..10]}.zip");
            var restorePoint = new RestorePoint();
            var files = new List<string>();
            using ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update);

            foreach (var jobObjectFile in jobObject.Files)
            {
                var name = Path.GetFileName(jobObjectFile);
                archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
                files.Add($"{term}_{name}");
            }

            archive.Dispose();

            foreach (var fileName in files)
            {
                restorePoint.AddFile(zipToOpen, fileName);
            }

            Log.Information($"restore point created successfully ");
            return restorePoint;
        }
    }
}