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
                { @"C:\Users\djhit\Desktop\University\oop trash\jobObjects\txtToCopy.txt",
                    @"C:\Users\djhit\Desktop\University\oop trash\jobObjects\txtToCopy2.txt"};
            _factory = new JobFactory(_backupService);
            _jobObject = _factory.CreateJobObject(files);
        }

        [Test]
        public void CreateBackupJob_InvokeJobWithSameRestorepoint_ThrowException()
        {
            var backupJob = BackupJobBuilder
                            .Init(_backupService)
                            .SetName("joba")
                            .SetAlgorithm(_context)
                            .SetRestorePoint(new RestorePoint() { JobObject = _jobObject} )
                            .Build();
            
            var backup = _backupService
                .CreateBackup("first backup", backupJob.Id, @"C:\Users\djhit\Desktop\University\oop trash\repository");
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
            _backupService.AddRestorePoint(backupJob.Id, _factory.CreateRestorePoint(_jobObject));
            _backupService.InvokeBackup(backup.Id, backupJob.Id);
        }
    }
}