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
        private JobObject _jobObject;
        private JobFactory _factory;
        
        [SetUp]
        public void Setup()
        {
            _backupService = new BackupService();
            _context = new Context(new SplitStorageAlgo());
            List<string> files = new() { @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\wwwroot\site.css" };
            _factory = new JobFactory(_backupService);
            _jobObject = _factory.CreateJobObject(files);
        }

        [Test]
        public void CreateBackupJob()
        {
            var backupJob = BackupJobBuilder
                            .Init(_backupService)
                            .SetName("joba")
                            .SetAlgorithm(_context)
                            .SetRestorePoint(new RestorePoint() { JobObject = _jobObject} )
                            .Build();
            
            var backup = _backupService
                .CreateBackup("first backup", backupJob.Id, @"C:\Users\djhit\RiderProjects\is\Kerenery\Backups\restore");
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
        }
    }
}