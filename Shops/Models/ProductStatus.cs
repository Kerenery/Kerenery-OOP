using Shops.Tools;

namespace Shops.Models
{
    public class ProductStatus
    {
        private int _count;
        private decimal _price;

        public ProductStatus(int count, decimal price)
        {
            Count = count;
            Price = price;
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value < 1)
                    throw new ShopException("wrong count");

                _count = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ShopException("The price is too low");
                }

                _price = value;
            }
        }

        public void ChangePrice(decimal money)
        {
            Price += money;
        }
    }
}