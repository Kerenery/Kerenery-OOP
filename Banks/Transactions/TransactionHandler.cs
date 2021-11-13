using Banks.Accounts;
using Banks.Models;
using Banks.SnapShot;
using Banks.Tools;

namespace Banks.Transactions
{
    public abstract class TransactionHandler : ITransactionHandler
    {
        protected TransactionHandler(Bank sender, Bank receiver)
        {
            Receiver = receiver;
            Sender = sender;
        }

        protected Caretaker AccountCaretaker { get; set; }

        protected ITransactionHandler NextHandler { get; set; }

        protected Bank Receiver { get; }

        protected Bank Sender { get; }

        public ITransactionHandler SetNext(ITransactionHandler handler)
        {
            NextHandler = handler ?? throw new BanksException("handler must be implemented");
            return handler;
        }

        public virtual decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver)
            => NextHandler.Handle(money, accountSender, accountReceiver);
    }
}