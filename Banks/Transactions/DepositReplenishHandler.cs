using System;
using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public class DepositReplenishHandler : TransactionHandler
    {
        public DepositReplenishHandler(Bank sender, Bank receiver = null)
            : base(sender, receiver)
        {
        }

        public override decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver = null)
        {
            if (accountSender is not DepositAccount depositAccount)
                return base.Handle(money, accountSender, accountReceiver);
            AccountCaretaker = new Caretaker() { Account = depositAccount };
            AccountCaretaker.Backup();
            if (depositAccount.CurrentBalance.WithdrawBalance <= money || DateTime.Now < depositAccount.PayDay)
                throw new BanksException("not enough money to withdraw, credit limit might be not enough");
            return depositAccount.UpdateBalance(money);
        }
    }
}