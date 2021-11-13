using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

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

            if (accountSender.CurrentBalance.WithdrawBalance <= money)
                throw new BanksException("not enough money to withdraw");

            accountSender.UpdateBalance(-money);
            return accountReceiver.UpdateBalance(money * (1 - Receiver.Commission));
        }
    }
}