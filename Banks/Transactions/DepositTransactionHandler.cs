using System;
using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public class DepositTransactionHandler : TransactionHandler
    {
        public DepositTransactionHandler(Bank sender, Bank receiver)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver)
        {
            if (accountSender is not DepositAccount depositAccount)
                return base.Handle(money, accountSender, accountReceiver);

            AccountCaretaker = new Caretaker() { Account = depositAccount };
            AccountCaretaker.Backup();
            if (depositAccount.CurrentBalance.WithdrawBalance < money || DateTime.Now < depositAccount.PayDay)
                throw new BanksException("not enough money to withdraw, The DAY of PAYDAY hasn't come yet");
            accountSender.UpdateBalance(-money);
            return accountReceiver.UpdateBalance(money * (1 - Receiver.Commission));
        }
    }
}