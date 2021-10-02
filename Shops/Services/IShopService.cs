using Shops.Models;

namespace Shops.Services
{
    public interface IShopService
    {
        Product RegisterProduct(string product);
        Product AddProduct(Shop shop, Product product, decimal price, int amount);
        void ChangePrice(Shop shop, Product product, decimal price);
        Shop FindCheapest(Product product, int amount);
        ShoppingList BuyGoods(Consumer consumer, ShoppingList goodsList, Shop shop);
    }
}