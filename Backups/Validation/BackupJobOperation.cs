using System;
using System.Collections.Generic;
using Backups.Models;

namespace Backups.Validation
{
    public class BackupJobOperation : Operation<BackupJob>
    {
        public BackupJobOperation(List<object> allowedActors, Action<BackupJob, object[]> action)
            : base(allowedActors, action)
        {
        }

        protected override bool Authorize(object actor) => _allowedActors.Contains(actor.GetType());
    }
}