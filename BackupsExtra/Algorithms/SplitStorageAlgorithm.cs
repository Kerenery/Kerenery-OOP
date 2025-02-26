﻿using System;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class SplitStorageAlgorithm : IAlgorithm
    {
        public AlgoType Type { get; } = AlgoType.SplitStorage;
        public RestorePoint Copy(JobObject jobObject, Repository repositoryToSave, int term)
        {
            var restorePoint = new RestorePoint()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
            };

            foreach (var jobObjectFile in jobObject.Files)
            {
                var zipToOpen = Path.Combine(repositoryToSave.Path, $"{Guid.NewGuid().ToString()[..10]}.zip");
                using ZipArchive archive = ZipFile.Open(zipToOpen, ZipArchiveMode.Update);
                var name = Path.GetFileName(jobObjectFile);
                archive.CreateEntryFromFile(jobObjectFile, Path.Combine(name, $"{term}_{name}"));
                restorePoint.AddFile(zipToOpen);
            }

            return restorePoint;
        }
    }
}