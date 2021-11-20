using System;

namespace BackupsExtra.Models
{
    public class Repository : IEquatable<Repository>
    {
        public string Path { get; init; }
        public bool Equals(Repository other)
            => other != null && Path == other.Path;
    }
}