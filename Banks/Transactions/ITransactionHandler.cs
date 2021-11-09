using Banks.Accounts;

namespace Banks.Transactions
{
    public interface ITransactionHandler
    {
        ITransactionHandler SetNext(ITransactionHandler handler);

        decimal Handle(decimal money, IAccount accountSender, IAccount accountReceiver);
    }
}