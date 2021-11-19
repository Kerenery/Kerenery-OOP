using System.Collections.Generic;

namespace BackupsExtra.Models
{
    public class Backup
    {
        private List<RestorePoint> _restorePoints = new ();
        public Repository Repository { get; init; }
    }
}