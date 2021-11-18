using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Algorithms;
using BackupsExtra.Models;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        [Test]
        public void FilesWorkCheck()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo restoreDirectory =
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "NewRestore"));
            var firstFile = File.Create(Path.Combine(currentDirectory, "1.txt"));
            var secondFile =  File.Create(Path.Combine(currentDirectory, "2.txt"));
            firstFile.Close();
            secondFile.Close();
            var algo = new SingleStorageAlgorithm();
            List<string> files = new()
            {
                Path.GetFullPath(firstFile.Name),
                Path.GetFullPath(secondFile.Name),
            };
            var jobObject = new JobObject() { Files = files };
            algo.Copy(jobObject, new Repository() { Path = restoreDirectory.FullName }, 1);
        }
    }
}