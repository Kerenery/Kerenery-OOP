using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(Balance newBalance)
            : base(newBalance)
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
    }
}