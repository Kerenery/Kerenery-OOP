using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Services;

namespace Backups.Models
{
    public class SplitStorageAlgo : IAlgorithm
    {
        public Storage CreateCopy(RestorePoint restorePoint, IRepository repository)
        {
            // var zipArchive = new ZipArchive();
            // foreach (var jobObjectFile in restorePoint.JobObject.Files)
            // {
            // }
            // // string apipa = restorePoint.JobObject.Files.Last().Replace(@"\\", @"\");
            // // ZipFile.CreateFromDirectory(@"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\wwwroot",  $@"{repository.Path}/some.zip");
            // // new ZipArchive()

            // using (FileStream zipToOpen = File.Open($@"{repository.Path}\{restorePoint.Id}", FileMode.Open))
            // {
            //     using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            //     {
            //         ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
            //         using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
            //         {
            //             writer.WriteLine("Information about this package.");
            //             writer.WriteLine("========================");
            //         }
            //     }
            // }
            using (FileStream zipToOpen = new FileStream(@$"{repository.Path}\\some.zip", FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    foreach (var jobObjectFile in restorePoint.JobObject.Files)
                    {
                        ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                        using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                        {
                            writer.WriteLine($"{jobObjectFile}");
                            writer.WriteLine("========================");
                        }
                    }
                }
            }

            return new Storage();
        }
    }
}