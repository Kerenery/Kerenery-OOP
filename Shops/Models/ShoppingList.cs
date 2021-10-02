using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Models
{
    public class ShoppingList
    {
        private readonly Dictionary<Product, int> _goods = new Dictionary<Product, int>();

        public void AddProduct(Product product, int count)
        {
            if (count < 1)
                throw new ShopException("forbidden count");

            if (_goods.Keys.Any(p => p.Id == product.Id))
                throw new ShopException("product is already in the list");

            _goods.Add(product, count);
        }

        public List<Product> Products() => _goods.Keys.ToList();
        public int GetCount(Product product) => _goods[product];
    }
}