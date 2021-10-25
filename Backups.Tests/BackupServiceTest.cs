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
            _context = new Context(new SingleStorageAlgo());
            List<string> files = new() { "gyyasd" };
            _factory = new JobFactory(_backupService);
            _jobObject = _factory.CreateJobObject(files);
        }

        [Test]
        public void CreateJob()
        {
            var backupJob = BackupJobBuilder
                            .Init(_backupService)
                            .SetName("joba")
                            .SetAlgorithm(_context)
                            .SetRestorePoint(new RestorePoint(_jobObject))
                            .Build();
        }
    }
}