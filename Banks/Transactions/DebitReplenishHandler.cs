using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public class DebitReplenishHandler : TransactionHandler
    {
        public DebitReplenishHandler(Bank sender, Bank receiver = null)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver = null)
        {
            if (accountSender is not DebitAccount debitAccount)
                return base.Handle(money, accountSender, accountReceiver);

            AccountCaretaker = new Caretaker() { Account = debitAccount };
            AccountCaretaker.Backup();
            if (debitAccount.CurrentBalance.WithdrawBalance <= money)
                throw new BanksException("not enough money to withdraw");
            return debitAccount.UpdateBalance(-money);
        }
    }
}