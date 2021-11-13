using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public abstract class Account : IAccount, IEquatable<Account>
    {
        protected Account(Balance newBalance, Guid holderId)
        {
            AccountId = Guid.NewGuid();
            HolderId = holderId;
            CurrentBalance = newBalance;
            OpenedOn = DateTime.Today;
        }

        public Guid AccountId { get; protected set; }
        public Guid HolderId { get; protected set; }
        public Balance CurrentBalance { get; protected set; }

        public DateTime OpenedOn { get; protected set; }

        public decimal UpdateBalance(decimal money)
        {
            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance + money };

            return CurrentBalance.WholeBalance;
        }

        public virtual IMemento Save()
            => new DebitAccountMemento() { CurrentBalance = CurrentBalance, AccountId = AccountId, HolderId = HolderId };

        public virtual void Restore(IMemento memento) { }

        public bool Equals(Account other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AccountId.Equals(other.AccountId) && HolderId.Equals(other.HolderId) && Equals(CurrentBalance, other.CurrentBalance) && OpenedOn.Equals(other.OpenedOn);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Account)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AccountId, HolderId, CurrentBalance, OpenedOn);
        }
    }
}