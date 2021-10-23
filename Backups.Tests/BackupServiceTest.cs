using System.Linq;
using Backups.Models;
using Backups.Services;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupServiceTest
    {
        private IBackupService _backupService;
        
        [SetUp]
        public void Setup()
        {
            _backupService = new BackupService();
        }

        [Test]
        public void CreateJob()
        {
        }
    }
}