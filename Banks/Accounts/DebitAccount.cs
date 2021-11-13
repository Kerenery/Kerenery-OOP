using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DebitAccount : Account, IEquatable<DebitAccount>
    {
        public DebitAccount(Balance newBalance, Guid holderId)
            : base(newBalance, holderId)
        {
        }

        public override void Restore(IMemento memento)
        {
            if (memento is not DebitAccountMemento)
                throw new BanksException("unknown memento");

            AccountId = memento.AccountId;
            HolderId = memento.HolderId;
            CurrentBalance = memento.CurrentBalance;
        }

        public bool Equals(DebitAccount other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DebitAccount)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}