using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(Balance newBalance, decimal creditLimit)
            : base(newBalance)
        {
            CreditLimit = creditLimit;
        }

        public decimal CreditLimit { get; private set; }

        public override decimal Withdraw(decimal money)
        {
            if (CurrentBalance.WithdrawBalance + CreditLimit <= money)
                throw new BanksException("not enough money to withdraw, credit limit might be not enough");

            CurrentBalance = new Balance()
                { FixedBalance = CurrentBalance.FixedBalance, WithdrawBalance = CurrentBalance.WithdrawBalance - money };
            return CurrentBalance.WholeBalance;
        }

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
    }
}