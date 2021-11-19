using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Algorithms;
using BackupsExtra.Models;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        private string _currentDirectory;
        private DirectoryInfo _restoreDirectory;
        private FileStream _firstFile;
        private FileStream _secondFile;
        private BackupExtraService _backupService;

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
            foreach(DirectoryInfo subDirectory in _restoreDirectory.GetDirectories()) subDirectory.Delete(true);
        }
        
        [Test]
        public void SingleAlgorithmCopyFilesWorkCheck()
        {
            var algo = new SingleStorageAlgorithm();
            
            List<string> files = new()
            {
                Path.GetFullPath(_firstFile.Name),
                Path.GetFullPath(_secondFile.Name),
            };
            
            var jobObject = new JobObject() { Files = files };
            algo.Copy(jobObject, new Repository() { Path = _restoreDirectory.FullName }, 1);
        }

        [Test]
        public void SplitAlgorithmCopyFilesWorkCheck()
        {
            var algo = new SplitStorageAlgorithm();
            
            List<string> files = new()
            {
                Path.GetFullPath(_firstFile.Name),
                Path.GetFullPath(_secondFile.Name),
            };
            
            var jobObject = new JobObject() { Files = files };
            algo.Copy(jobObject, new Repository() { Path = _restoreDirectory.FullName }, 1);
        }
    }
}