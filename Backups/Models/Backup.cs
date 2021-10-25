using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class Backup
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}