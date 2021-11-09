using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;

namespace Banks.Transactions
{
    public class WithdrawHandler : TransactionHandler
    {
        public WithdrawHandler(Bank sender, Bank receiver)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver)
        {
            if (accountSender is not DebitAccount)
                return base.Handle(money, accountSender, accountReceiver);

            AccountCaretaker = new Caretaker() { Account = accountSender };
            AccountCaretaker.Backup();
            accountSender.UpdateBalance(-money);
            return accountReceiver.UpdateBalance(money * (1 - Receiver.Commission));
        }
    }
}