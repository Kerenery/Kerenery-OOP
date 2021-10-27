using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Interfaces;
using Backups.Services;

namespace Backups.Models
{
    public class SingleStorageAlgo : IAlgorithm
    {
        public Storage CreateCopy(RestorePoint restorePoint, IRepository repository)
        {
            return null;
        }
    }
}