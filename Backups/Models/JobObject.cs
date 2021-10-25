using System;
using System.Collections.Generic;

namespace Backups.Models
{
    public class JobObject
    {
        public List<string> Files { get; init; }
        public Guid Id { get; init; }
    }
}