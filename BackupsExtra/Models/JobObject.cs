using System;
using System.Collections.Generic;

namespace BackupsExtra.Models
{
    public class JobObject
    {
        public List<string> Files { get; init; }
        public Guid Id { get; } = Guid.NewGuid();
    }
}