using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public class CreditTransactionHandler : TransactionHandler
    {
        public CreditTransactionHandler(Bank sender, Bank receiver)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver)
        {
            if (accountSender is not CreditAccount creditAccount)
                return base.Handle(money, accountSender, accountReceiver);
            AccountCaretaker = new Caretaker() { Account = creditAccount };
            AccountCaretaker.Backup();

            if (creditAccount.CurrentBalance.WithdrawBalance + creditAccount.CreditLimit <= money)
                throw new BanksException("not enough money to withdraw, credit limit might be not enough");
            accountSender.UpdateBalance(-money);
            return accountReceiver.UpdateBalance(money * (1 - Receiver.Commission));
        }
    }
}