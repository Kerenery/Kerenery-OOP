using System;
using System.Collections.Generic;

namespace Backups.Models
{
    public class RestorePoint
    {
        private readonly List<JobObject> _jobObjects;

        public RestorePoint(List<JobObject> jobObjects)
        {
            _jobObjects = jobObjects;
        }

        public DateTime CreationTime { get; init; }
        public Guid Id { get; init; }
    }
}