using System;
using Banks.SnapShot;

namespace Banks.Accounts
{
    public interface IAccount
    {
        public Balance CurrentBalance { get; }
        public decimal Withdraw(decimal money);
        public void Replenish(decimal money);
        public decimal Transfer(decimal money);

        public IMemento Save();

        public void Restore(IMemento memento);
    }
}