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

        public string JobName { get; private set; }

        public void SetAlgorithm(Context context)
        {
            if (!context.IsAlgorithmExists())
                throw new BackupException("algo is not chosen");

            Context = context;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || JobName is not null)
                throw new BackupException("name of job cant be null or already exists");

            JobName = name;
        }
    }
}