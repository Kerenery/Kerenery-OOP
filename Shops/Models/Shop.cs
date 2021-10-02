using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Shops.Tools;
using Shops.Validation;
#pragma warning disable SA1201

namespace Shops.Models
{
    public class Shop
    {
        private readonly Dictionary<Product, ProductStatus> _goods = new Dictionary<Product, ProductStatus>();
        private readonly string _id;
        private string _name;
        private string _address;

        public Shop(string name, string address)
        {
            _id = Guid.NewGuid().ToString();
            Name = name;
            Address = address;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string Address
        {
            get => _address;
            private set => _address = value;
        }

        public string Id => _id;
        public Operation<Shop> ChangePrice => new ShopOperation(
            new List<object> { typeof(ShopManager) },
            (shop, objects) => shop.DoChangePrice(objects[0] as Product, (decimal)objects[1]));
        public Operation<Shop> BuyGoods => new ShopOperation(
            new List<object> { typeof(ShopManager) },
            (shop, objects) => shop.DoBuyGoods(objects[0] as ShoppingList, objects[1] as Consumer));
        public Product AddProduct(Product product, decimal price, int count)
        {
            if (_goods.Keys.Any(p => p.Id == product.Id))
                throw new ShopException("The product is already exists");

            _goods.Add(product, new ProductStatus(count, price));
            return product;
        }

        public Product FindProduct(Product product) => _goods.Keys.FirstOrDefault(p => p.Id == product.Id);
        public ProductStatus ProductStatus(Product product) => _goods[product];
        private void DoChangePrice(Product product, decimal price) => _goods[product].Price = price;

        private void DoBuyGoods(ShoppingList shoppingList, Consumer consumer)
        {
            if (shoppingList.Products().Any(product => FindProduct(product) == null))
                throw new ShopException("There is no such product in the shop");

            if (shoppingList.Products().Any(product => _goods[product].Count < shoppingList.GetCount(product)))
                throw new ShopException("There is not enough products in the shop");

            decimal totalPrice = shoppingList.Products()
                .Sum(product => _goods[product].Price * shoppingList.GetCount(product));

            if (consumer.Balance < totalPrice)
                throw new ShopException("Not enough money");

            foreach (var product in shoppingList.Products())
            {
                _goods[FindProduct(product)].Count -= shoppingList.GetCount(product);
            }

            consumer.Balance -= totalPrice;
        }
    }
}