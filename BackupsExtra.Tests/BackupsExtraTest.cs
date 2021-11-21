using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Algorithms;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Services;
using BackupsExtra.Snapshot;
using NUnit.Framework;
using Serilog;
using Serilog.Formatting.Json;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        private string _currentDirectory;
        private DirectoryInfo _restoreDirectory;
        private FileStream _firstFile;
        private FileStream _secondFile;
        private BackupExtraService _backupService;
        private JobObject _jobObject;
        private Keeper _backupKeeper;
        private AlgorithmFactory _algorithmFactory;
        private ICleaningAlgorithm _cleaningAlgorithm;

        [SetUp]
        public void Setup()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _restoreDirectory =
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NewRestore"));
            _firstFile = File.Create(Path.Combine(_currentDirectory, "1.txt"));
            _secondFile =  File.Create(Path.Combine(_currentDirectory, "2.txt"));
            _firstFile.Close();
            _secondFile.Close();
            _backupService = new BackupExtraService();
            foreach(FileInfo file in _restoreDirectory.GetFiles()) file.Delete();
            foreach(DirectoryInfo subDirectory in _restoreDirectory.GetDirectories()) subDirectory.Delete(false);

            _backupKeeper = new Keeper(_backupService);
            _algorithmFactory = new AlgorithmFactory();
            _cleaningAlgorithm = _algorithmFactory
                .CreateCleaningAlgorithm(Limit.RestorePoints, pointsLimit: 1);
                        
            List<string> files = new()
            {
                Path.GetFullPath(_firstFile.Name),
                Path.GetFullPath(_secondFile.Name),
            };
            
            _jobObject = new JobObject() { Files = files };
            
                                    
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), Path.Combine(Directory.GetCurrentDirectory(), "log.json"))
                .CreateLogger();
        }
        

        [Test]
        public void BackupJobProcessWorkCheck()
        {
            var backupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SingleStorageAlgorithm())
                .SetName("иногда просто не хватает кусочка питсы")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();

            var newBackupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SingleStorageAlgorithm())
                .SetName("я просто напросто затупок и ничего более")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();

            var cleanJob = CleanJobBuilder.Init(_backupService)
                .SetName("я клининг джобайден")
                .SetAlgorithm(_cleaningAlgorithm)
                .SetBackupToClean(_backupService.FindBackup("backup[0]").Id)
                .Build();
            
            _backupKeeper.Backup();
            _backupKeeper.Restore();
        }
    }
}