using System.Collections.Generic;
using Backups.Services;

namespace Backups.Models
{
    public class SingleStorageAlgo : IAlgorithm
    {
        public RestorePoint CreateCopy(JobObject job)
        {
            throw new System.NotImplementedException();
        }
    }
}