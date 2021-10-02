using Shops.Tools;

namespace Shops.Models
{
    public class Consumer
    {
        private decimal _balance;

        public Consumer(decimal money)
        {
            Balance = money;
        }

        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new ShopException("Balance cant be negative");
                }

                _balance += value;
            }
        }
    }
}