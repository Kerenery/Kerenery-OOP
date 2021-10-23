using System;
using System.Collections.Generic;
using Backups.Tools;

#pragma warning disable SA1401
#pragma warning disable SA1202

namespace Backups.Validation
{
    public abstract class Operation<T>
    {
        protected readonly List<object> _allowedActors;

        private Action<T, object[]> _action;

        public Operation(List<object> allowedActors, Action<T, object[]> action)
        {
            _allowedActors = allowedActors;
            _action = action;
        }

        protected abstract bool Authorize(object actor);

        public void PerformOperation(object actor, T self, params object[] args)
        {
            if (!Authorize(actor))
                throw new BackupException("Invalid validation");

            _action?.Invoke(self, args);
        }
    }
}