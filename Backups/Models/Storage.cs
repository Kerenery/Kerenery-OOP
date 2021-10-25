using Backups.Interfaces;

namespace Backups.Models
{
    public class Storage : IRepository
    {
        public string Path { get; init; }
    }
}