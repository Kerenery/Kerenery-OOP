using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public abstract class Account : IAccount
    {
        protected Account(Balance newBalance)
        {
            AccountId = Guid.NewGuid();
            HolderId = Guid.NewGuid();
            CurrentBalance = newBalance;
        }

        public Guid AccountId { get; protected set; }
        public Guid HolderId { get; protected set; }
        public Balance CurrentBalance { get; protected set; }

        public DateTime OpenedOn { get; init;  }

        public decimal UpdateBalance(decimal money)
        {
            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance + money };

            return CurrentBalance.WholeBalance;
        }

        public virtual IMemento Save()
            => new DebitAccountMemento() { CurrentBalance = CurrentBalance, AccountId = AccountId, HolderId = HolderId };

        public virtual void Restore(IMemento memento) { }
    }
}