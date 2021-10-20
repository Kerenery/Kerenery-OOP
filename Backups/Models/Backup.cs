using System.Collections.Generic;

namespace Backups.Models
{
    public abstract class Backup
    {
        private LinkedList<RestorePoint> _restorePoints = new LinkedList<RestorePoint>();
    }
}