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
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.RegisterProduct("nesskkbiq");
                _shopManager.RegisterProduct("nesskkbiq");
            });
        }

        [Test]
        public void FindCheapest()
        {
            var firstShop = new Shop("Kikimora", "Barren");
            var secondShop = new Shop("Tengu", "Gas");
            _shopManager.RegisterProduct("moonlight energy drink");
            _shopManager.RegisterShop(firstShop);
            _shopManager.RegisterShop(secondShop);
            _shopManager.AddProduct(firstShop, _shopManager.FindRegisteredProduct("moonlight energy drink"), 50, 20);
            _shopManager.AddProduct(secondShop, _shopManager.FindRegisteredProduct("moonlight energy drink"), 49, 10);
            Console.WriteLine(_shopManager.FindCheapest(_shopManager.FindRegisteredProduct("moonlight energy drink"), 12).Name); 
        }

        [Test]
        public void ChangePrice()
        {
            var shop = new Shop("Megathron", "Ice");
            _shopManager.RegisterProduct("super cool staff");
            _shopManager.RegisterShop(shop);
            _shopManager.AddProduct(shop, _shopManager.FindRegisteredProduct("super cool staff"), 300, 20);
            _shopManager.ChangePrice(shop, _shopManager.FindRegisteredProduct("super cool staff"), 200);
        }

        [Test]
        public void BuySomething_NotEnoughMoney_ThrowException()
        {
            var consumer = new Consumer(4);
            var productList = new ShoppingList();
            var shop = new Shop("Garmur", "Lava");
            
            _shopManager.RegisterShop(shop);
            _shopManager.RegisterProduct("zyabl");
            _shopManager.RegisterProduct("shih");
            _shopManager.AddProduct(shop, _shopManager.FindRegisteredProduct("zyabl"), 20, 20);
            _shopManager.AddProduct(shop, _shopManager.FindRegisteredProduct("shih"), 20, 20);
            
            productList.AddProduct(_shopManager.FindRegisteredProduct("zyabl"), 2);
            productList.AddProduct(_shopManager.FindRegisteredProduct("shih"), 2);

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
            var shop = new Shop("Rorqual", "Plasma");
            
            _shopManager.RegisterShop(shop);
            _shopManager.RegisterProduct("zyabl");
            _shopManager.RegisterProduct("shih");
            _shopManager.AddProduct(shop, _shopManager.FindRegisteredProduct("zyabl"), 20, 20);
            _shopManager.AddProduct(shop, _shopManager.FindRegisteredProduct("shih"), 20, 20);
            
            productList.AddProduct(_shopManager.FindRegisteredProduct("zyabl"), 21);
            productList.AddProduct(_shopManager.FindRegisteredProduct("shih"), 23);
            
            Assert.Catch<ShopException>(() =>
            {
                _shopManager.BuyGoods(consumer, productList, shop);
            });
        }
    }
}