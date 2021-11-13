using System;
using Banks.SnapShot;

namespace Banks.Accounts
{
    public interface IAccount
    {
        Guid AccountId { get; }
        Guid HolderId { get; }
        DateTime OpenedOn { get; }
        Balance CurrentBalance { get; }

        IMemento Save();

        void Restore(IMemento memento);

        decimal UpdateBalance(decimal money);
    }
}