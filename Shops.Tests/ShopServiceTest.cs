using System;
using Shops.Models;
using Shops.Services;
using Shops.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void RegisterSameProductTwice_ThrowException()
        {
            var product = new Product("neskvik");
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.RegisterProduct(product);
                _shopManager.RegisterProduct(product);
            });
        }

        [Test]
        public void FindCheapest()
        {
            var firstShop = new Shop("Kikimora", "Barren");
            var secondShop = new Shop("Tengu", "Gas");
            var product = new Product("moonlight energy drink");
            _shopManager.RegisterProduct(product);
            _shopManager.RegisterShop(firstShop);
            _shopManager.RegisterShop(secondShop);
            _shopManager.AddProduct(firstShop, product, 50, 20);
            _shopManager.AddProduct(secondShop, product, 49, 10);
            Console.WriteLine(_shopManager.FindCheapest(product, 12).Name); 
        }

        [Test]
        public void ChangePrice()
        {
            var shop = new Shop("Megathron", "Ice");
            var product = new Product("super cool staff");
            _shopManager.RegisterProduct(product);
            _shopManager.RegisterShop(shop);
            _shopManager.AddProduct(shop, product, 300, 20);
            _shopManager.ChangePrice(shop, product, 200);
        }

        [Test]
        public void BuySomething_NotEnoughMoney_ThrowException()
        {
            var consumer = new Consumer(4);
            var productList = new ShoppingList();
            var firstProduct = new Product("zyabl");
            var secondProduct = new Product("shih");
            var shop = new Shop("Garmur", "Lava");
            
            _shopManager.RegisterShop(shop);
            _shopManager.RegisterProduct(firstProduct);
            _shopManager.RegisterProduct(secondProduct);
            _shopManager.AddProduct(shop, firstProduct, 20, 20);
            _shopManager.AddProduct(shop, secondProduct, 20, 20);
            
            productList.AddProduct(firstProduct, 2);
            productList.AddProduct(secondProduct, 2);

            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyGoods(consumer, productList, shop);
            });
        }

        [Test]
        public void BuyGoods_NotEnoughCount_ThrowException()
        {
            var consumer = new Consumer(4000000);
            var productList = new ShoppingList();
            var firstProduct = new Product("zyabl");
            var secondProduct = new Product("shih");
            var shop = new Shop("Rorqual", "Plasma");
            
            _shopManager.RegisterShop(shop);
            _shopManager.RegisterProduct(firstProduct);
            _shopManager.RegisterProduct(secondProduct);
            _shopManager.AddProduct(shop, firstProduct, 20, 20);
            _shopManager.AddProduct(shop, secondProduct, 20, 20);
            
            productList.AddProduct(firstProduct, 21);
            productList.AddProduct(secondProduct, 23);
            
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyGoods(consumer, productList, shop);
            });
        }
    }
}