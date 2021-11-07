using System;
using Banks.Accounts;

namespace Banks.SnapShot
{
    public interface IMemento
    {
        Guid AccountId { get; init; }
        Guid HolderId { get; init; }
        Balance CurrentBalance { get; init; }
    }
}