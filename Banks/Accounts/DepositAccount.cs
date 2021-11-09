using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(Balance newBalance, DateTime payDay)
            : base(newBalance)
        {
            PayDay = payDay;
        }

        public DateTime PayDay { get; private set; }

        public override IMemento Save()
            => new DepositAccountMemento()
            {
                CurrentBalance = CurrentBalance, AccountId = AccountId,
                PayDay = PayDay, HolderId = HolderId,
            };

        public override void Restore(IMemento memento)
        {
            if (memento is not DepositAccountMemento accountMemento)
                throw new BanksException("unknown memento");

            AccountId = accountMemento.AccountId;
            HolderId = accountMemento.HolderId;
            CurrentBalance = accountMemento.CurrentBalance;
            PayDay = accountMemento.PayDay;
        }
    }
}