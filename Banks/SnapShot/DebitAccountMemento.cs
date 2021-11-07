using System;
using Banks.Accounts;

namespace Banks.SnapShot
{
    public class DebitAccountMemento : IMemento
    {
        public Guid AccountId { get; init; }
        public Guid HolderId { get; init; }
        public Balance CurrentBalance { get; init; }

        public DateTime PayDay { get; init; }
    }
}