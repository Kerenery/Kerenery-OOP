using System;
using System.Collections.Generic;

namespace Backups.Models
{
    public class RestorePoint
    {
        private readonly List<string> _files = new List<string>();
        public DateTime CreationTime { get; init; }
        public Guid Id { get; init; }
    }
}