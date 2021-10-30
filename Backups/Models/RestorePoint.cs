using System;
using System.Collections.Generic;

namespace Backups.Models
{
    public class RestorePoint
    {
        public JobObject JobObject { get; init; }

        public DateTime CreationTime { get; init; }
        public Guid Id { get; init; }
    }
}