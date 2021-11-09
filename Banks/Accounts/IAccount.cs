using System;
using Banks.SnapShot;

namespace Banks.Accounts
{
    public interface IAccount
    {
        public Balance CurrentBalance { get; }

        public IMemento Save();

        public void Restore(IMemento memento);

        public decimal UpdateBalance(decimal money);
    }
}