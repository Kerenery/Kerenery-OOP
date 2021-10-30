using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class Backup
    {
        private LinkedList<RestorePoint> _restorePoints = new LinkedList<RestorePoint>();
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}