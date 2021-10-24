using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        public Context Context { get; init; }
        public Guid Id { get; init; }
    }
}