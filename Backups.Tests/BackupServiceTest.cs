using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupServiceTest
    {
        private BackupService _backupService;
        private Context _context;
        private Context _splitContext;
        private JobObject _jobObject;
        private JobFactory _factory;

        [SetUp]
        public void Setup()
        {
            _backupService = new BackupService();
            _context = new Context(new SingleStorageAlgo());
            _splitContext = new Context(new SplitStorageAlgo());
            List<string> files = new()
            {
                @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\filesToBackup\1.txt",
                @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\filesToBackup\2.txt"
            };
            _factory = new JobFactory(_backupService);
            _jobObject = _factory.CreateJobObject(files);
        }

        [Test]
        public void CreateBackupJob_InvokeBackup()
        {
            var backupJob = BackupJobBuilder
                .Init(_backupService)
                .SetName("joba")
                .SetAlgorithm(_context)
                .SetRestorePoint(new RestorePoint() { JobObject = _jobObject })
                .Build();

            var backup = _backupService
                .CreateBackup("first backup", backupJob.Id,
                    @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\backup");
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
            // second iteration, get _2file and _3file, then delete one of files
            _backupService.AddRestorePoint(backupJob.Id, _factory.CreateRestorePoint(_jobObject));
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
            _backupService.AddRestorePoint(backupJob.Id, _factory.CreateRestorePoint(_jobObject));
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
            _jobObject.Files.Remove(@"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\filesToBackup\2.txt");
            _backupService.AddRestorePoint(backupJob.Id, _factory.CreateRestorePoint(_jobObject));
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
        }

        [Test]
        public void FirstNotionTest()
        {
            var backupJob = BackupJobBuilder
                .Init(_backupService)
                .SetName("joba")
                .SetAlgorithm(_splitContext)
                .SetRestorePoint(new RestorePoint() { JobObject = _jobObject })
                .Build();

            var backup = _backupService.CreateBackup("second backup", backupJob.Id,
                @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\backup");
            _backupService.InvokeBackup(backup.Id, backupJob.Id);

            _jobObject.Files.Remove(@"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\filesToBackup\2.txt");
            _backupService.AddRestorePoint(backupJob.Id, _factory.CreateRestorePoint(_jobObject));
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
        }

        [Test]
        public void SecondNotionTest()
        {
            var backupJob = BackupJobBuilder
                .Init(_backupService)
                .SetName("joba")
                .SetAlgorithm(_context)
                .SetRestorePoint(new RestorePoint() { JobObject = _jobObject })
                .Build();
            
            var backup = _backupService.CreateBackup("second backup", backupJob.Id,
                @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore\backup");
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
        }
    }
}