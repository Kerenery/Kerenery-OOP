using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Services;
using Shops.Tools;

namespace Shops.Models
{
    public class ShopManager : IShopService
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Product> _registeredGoods = new List<Product>();

        public void RegisterProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ShopException("Name cant be null");

            if (_registeredGoods.Any(p => p.Id == product.Id))
                throw new ShopException("Product is already registered");

            _registeredGoods.Add(product);
        }

        public void RegisterShop(Shop shop)
        {
            if (string.IsNullOrWhiteSpace(shop.Name))
                throw new ShopException("Name cant be null");

            if (_shops.Any(s => s.Id == shop.Id))
                throw new ShopException("Shop is already registered");

            _shops.Add(shop);
        }

        public Product AddProduct(Shop shop, Product product, decimal price, int amount)
        {
            if (_shops.All(s => s.Id != shop.Id))
                throw new ShopException("There is no such shop");

            if (_registeredGoods.All(p => p.Id != product.Id))
                throw new ShopException("The product is not registered");

            shop.AddProduct(product, price, amount);
            return product;
        }

        public void ChangePrice(Shop shop, Product product, decimal price)
        {
            if (_shops.All(s => s.Id != shop.Id))
                throw new ShopException("There is no such shop");

            if (_registeredGoods.All(p => p.Id != product.Id))
                throw new ShopException("The product is not registered");

            if (shop.FindProduct(product) == null)
                throw new ShopException("The product is not added yet");

            shop.ChangePrice.PerformOperation(this, shop, product, price);
        }

        public Shop FindCheapest(Product product, int amount)
        {
            if (_registeredGoods.All(p => p.Id != product.Id))
                throw new ShopException("Something wrong, i can feel it");

            return _shops.OrderBy(s => s.ProductStatus(product).Price).FirstOrDefault(s => s.ProductStatus(product).Count >= amount)
                   ?? throw new ShopException("Not enough goods, sry");
        }

        public ShoppingList BuyGoods(Consumer consumer, ShoppingList goodsList, Shop shop)
        {
            shop.BuyGoods.PerformOperation(this, shop, goodsList, consumer);
            return goodsList;
        }
    }
}