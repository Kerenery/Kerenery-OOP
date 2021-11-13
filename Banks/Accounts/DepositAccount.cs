using System;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class DepositAccount : Account, IEquatable<DepositAccount>
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

        public bool Equals(DepositAccount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && PayDay.Equals(other.PayDay);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DepositAccount)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PayDay);
        }
    }
}