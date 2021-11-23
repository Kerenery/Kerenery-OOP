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
using BackupsExtra.Tools;
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
        private ICleaningAlgorithm _pointCountCleaningAlgorithm;
        private ICleaningAlgorithm _wrongDateCleaningAlgorithm;
        private ICleaningAlgorithm _correctDateCleaningAlgorithm;

        [SetUp]
        public void Setup()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _restoreDirectory =
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NewRestore"));
            _firstFile = File.Create(Path.Combine(_currentDirectory, "1.txt"));
            _secondFile = File.Create(Path.Combine(_currentDirectory, "2.txt"));
            _firstFile.Close();
            _secondFile.Close();
            _backupService = new BackupExtraService();
            foreach (FileInfo file in _restoreDirectory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in _restoreDirectory.GetDirectories()) subDirectory.Delete(false);

            _backupKeeper = new Keeper(_backupService);
            _algorithmFactory = new AlgorithmFactory();
            _pointCountCleaningAlgorithm = _algorithmFactory
                .CreateCleaningAlgorithm(Limit.RestorePoints, pointsLimit: 1);
            _wrongDateCleaningAlgorithm = _algorithmFactory
                .CreateCleaningAlgorithm(Limit.DateLimit, date: DateTime.Now.AddDays(1));
            _correctDateCleaningAlgorithm = _algorithmFactory
                .CreateCleaningAlgorithm(Limit.DateLimit, DateTime.Today.AddDays(-1));

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
                .SetAlgorithm(_pointCountCleaningAlgorithm)
                .SetBackupToClean(_backupService.FindBackup("backup[0]").Id)
                .Build();

            _backupKeeper.Backup();
            _backupKeeper.Restore();
        }

        [Test]
        public void SplitStorageMergeCheck()
        {
            var backupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SplitStorageAlgorithm())
                .SetName("love me anywhere ninja, anywhere")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();

            _backupKeeper.Backup();
            _backupKeeper.Restore();

            var anotherBackupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SplitStorageAlgorithm())
                .SetName("even in my bum, even in my bum")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();
        }

        [Test]
        public void DateCleaning_DeleteAllPoints_ThrowException()
        {
            _backupKeeper.Restore();
            
            Assert.Catch<BackupsExtraException>(() =>
            {
                var cleanJob = CleanJobBuilder.Init(_backupService)
                    .SetName("я клининг джобайден")
                    .SetAlgorithm(_wrongDateCleaningAlgorithm)
                    .SetBackupToClean(_backupService.FindBackup("backup[0]").Id)
                    .Build();
            });
        }


        [Test]
        public void ShouldSaveAndLoadBackupsState_Successful()
        {
            var firstBackup = _backupService
                .AddBackup("tiny backup", Path.Combine(Directory.GetCurrentDirectory(), "firstBackup"));
            var secondBackup = _backupService
                .AddBackup("tiny backup", Path.Combine(Directory.GetCurrentDirectory(), "thirdBackup"));
            var thirdBackup = _backupService
                .AddBackup("tiny backup", Path.Combine(Directory.GetCurrentDirectory(), "fourthBackup"));
            
            var expectedBackupsIds = new[]
            {
                firstBackup.Id, 
                secondBackup.Id, 
                thirdBackup.Id,
            };
            
            expectedBackupsIds = expectedBackupsIds.OrderBy(x => x).ToArray();

            _backupKeeper.Backup();
            _backupKeeper.Restore();

            var actual = _backupService.GetBackups()
                .Select(x => x.Id)
                .OrderBy(x => x)
                .ToArray();

            CollectionAssert.AreEqual(expectedBackupsIds, actual);
        }

        [Test]
        public void SplitBubblingCleaningCheck()
        {
            var backupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SplitStorageAlgorithm())
                .SetName("love me anywhere ninja, anywhere")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();
            
            var anotherBackupJob = BackupJobBuilder.Init(_backupService)
                .SetAlgorithm(new SplitStorageAlgorithm())
                .SetName("love me anywhere ninja, anywhere")
                .SetJobObject(_jobObject)
                .ToDestination(_restoreDirectory.FullName)
                .Build();
            
            var cleanJob = CleanJobBuilder.Init(_backupService)
                .SetName("split papasha")
                .SetAlgorithm(_pointCountCleaningAlgorithm)
                .SetBackupToClean(_backupService.FindBackup("backup[0]").Id)
                .Build();
        }
    }
}