using System;

namespace BackupsExtra.Models
{
    public class Repository : IEquatable<Repository>
    {
        public string Path { get; init; }
        public bool Equals(Repository other)
            => other != null && Path == other.Path;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return obj is Repository repository && Equals(repository);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}