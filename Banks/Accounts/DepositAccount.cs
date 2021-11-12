using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        public DepositAccount(Balance newBalance, Guid holderId, DateTime payDay)
            : base(newBalance, holderId)
        {
            PayDay = payDay;
        }

        public DateTime PayDay { get; private set; }
        public void SkipMonth() => OpenedOn = OpenedOn.AddMonths(-1);

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