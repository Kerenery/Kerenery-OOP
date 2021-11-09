using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public class CreditReplenishHandler : TransactionHandler
    {
        public CreditReplenishHandler(Bank sender, Bank receiver = null)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver = null)
        {
            if (accountSender is not CreditAccount creditAccount)
                return base.Handle(money, accountSender, accountReceiver);
            AccountCaretaker = new Caretaker() { Account = creditAccount };
            AccountCaretaker.Backup();
            if (creditAccount.CurrentBalance.WithdrawBalance + creditAccount.CreditLimit <= money)
                throw new BanksException("not enough money to withdraw, credit limit might be not enough");
            return creditAccount.UpdateBalance(-money);
        }
    }
}