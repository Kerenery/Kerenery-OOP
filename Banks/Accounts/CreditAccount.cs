using System;
using Banks.SnapShot;
using Banks.Tools;
using Newtonsoft.Json;

namespace Banks.Accounts
{
    public class CreditAccount : Account, IEquatable<CreditAccount>
    {
        public CreditAccount(Balance newBalance, Guid holderId, decimal creditLimit)
            : base(newBalance, holderId)
        {
            CreditLimit = creditLimit;
        }

        public decimal CreditLimit { get; private set; }

        public override IMemento Save()
            => new CreditAccountMemento()
                {
                    CurrentBalance = CurrentBalance, AccountId = AccountId,
                    CreditLimit = CreditLimit, HolderId = HolderId,
                };

        public override void Restore(IMemento memento)
        {
            if (memento is not CreditAccountMemento accountMemento)
                throw new BanksException("unknown memento");

            AccountId = accountMemento.AccountId;
            HolderId = accountMemento.HolderId;
            CurrentBalance = accountMemento.CurrentBalance;
            CreditLimit = accountMemento.CreditLimit;
        }

        public bool Equals(CreditAccount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && CreditLimit == other.CreditLimit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CreditAccount)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), CreditLimit);
        }
    }
}