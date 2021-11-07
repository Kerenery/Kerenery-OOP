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

        public virtual decimal Withdraw(decimal money)
        {
            if (CurrentBalance.WithdrawBalance <= money)
                throw new BanksException("not enough money to withdraw");

            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance - money };
            return CurrentBalance.WholeBalance;
        }

        public void Replenish(decimal money)
        {
            if (money <= 0)
                throw new BanksException($"u cant proceed such operation, {money} is below zero");

            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance + money };
        }

        public virtual decimal Transfer(decimal money)
        {
            if (CurrentBalance.WithdrawBalance < money)
                throw new BanksException("not enough money to transfer");

            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance - money };

            return money;
        }

        public virtual IMemento Save()
            => new DebitAccountMemento() { CurrentBalance = CurrentBalance, AccountId = AccountId, HolderId = HolderId };

        public virtual void Restore(IMemento memento) { }
    }
}