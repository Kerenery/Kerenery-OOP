using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Tools;

namespace Backups.Models
{
    public class BackupJob
    {
        public Context Context { get; private set; }
        public Guid Id { get; init; }

        public void SetAlgorithm(Context context)
        {
            if (!context.IsAlgorithmExists())
                throw new BackupException("algo is not chosen");

            Context = context;
        }
    }
}