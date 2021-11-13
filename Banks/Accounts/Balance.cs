namespace Banks.Accounts
{
    public class Balance
    {
        public decimal WithdrawBalance { get; init; }
        public decimal FixedBalance { get; init;  }

        public decimal WholeBalance => WithdrawBalance + FixedBalance;
    }
}