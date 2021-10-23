using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;
using Backups.Validation;

namespace Backups.Models
{
    public class BackupJob
    {
        private readonly LinkedList<RestorePoint> _graphPoint;
        private Context _context;

        public BackupJob(Context context, LinkedList<RestorePoint> graphPoint)
        {
            _context = context;
            _graphPoint = graphPoint;
        }

        public Guid Id { get; init; }

        public Operation<BackupJob> GetNodes => new BackupJobOperation(
            new List<object> { typeof(BackupJob) },
            (backupJob, objects) => backupJob.DoGetNodes(objects[0] as Backup));

        public RestorePoint AddRestorePoint(RestorePoint restorePoint)
        {
            if (_graphPoint.Any(rp => rp.Id == restorePoint.Id))
                throw new BackupException("such point is already added");

            _graphPoint.AddLast(restorePoint);
            return restorePoint;
        }

        private LinkedList<RestorePoint> DoGetNodes(Backup backup) => _graphPoint;
    }
}