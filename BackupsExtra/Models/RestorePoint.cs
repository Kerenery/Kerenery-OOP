using System;
using System.Collections.Generic;

namespace BackupsExtra.Models
{
    public class RestorePoint
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.Now;
        public List<string> Files { get; init; }
    }
}