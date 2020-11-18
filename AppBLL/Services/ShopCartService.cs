using App_Model.Models;
using AppBLL.Interfaces;
using AppBLL.VMs;
using AppDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Services
{
    public class ShopCartService : IShopCartService
    {
        private MyShopDbContext db { get; set; }
        public ShopCartService()
        {
            db = new MyShopDbContext();
        }
        public Guid CreateShopCart()
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Capacity = 50
            };
            db.ShoppingCart.Add(shoppingCart);
            db.SaveChanges();
            return shoppingCart.Id;
        }

        public void AddInShopCart(Guid shopCartId, Guid productId, float amount, decimal price)
        {
            var shopCartItem = new ShoppingCartItem() 
            {
                ShoppingCartId = shopCartId,
                ProductId = productId,
                Amount = amount,
                Price = price
            };
            db.ShoppingCartItem.Add(shopCartItem);
            db.SaveChanges();
        }

        public Guid GetShopCart(Guid userId)
        {
            return (db.Buyer.Find(userId)).ShoppingCartId;
             
        }

        public List<ShopCartItemVM> GetPageInfo(int start_items, int amount_items, Guid shopCartId)
        {
            List<ShopCartItemVM> sciVMs = new List<ShopCartItemVM>();

            var scis = db.ShoppingCartItem
                               .Where(m => m.ShoppingCartId == shopCartId)
                               .OrderBy(p => p.Id)
                               .Skip(start_items)
                               .Take(amount_items)
                               .ToList();

            for (var i = 0; i < scis.Count(); i++)
            {
                var item = new ShopCartItemVM() 
                {
                    Id = scis[i].Id,
                    Amount = scis[i].Amount,

                    Product = new ProductService().FindProduct(scis[i].ProductId.Value),
                    Price = scis[i].Price,


                };
                sciVMs.Add(item);

            }
            return sciVMs;

        }


        public int GetNumbOfItem(Guid shopCartId)
        {
            
            var items = (from t in db.ShoppingCartItem
                         .Where(m => m.ShoppingCartId == shopCartId)
                         select t);
            return items.Count();
        }
        public List<ShopCartItemVM> GetAllItems(Guid shopCartId)
        {
            List<ShopCartItemVM> itemsVMs = new List<ShopCartItemVM>();

            var items = (from t in db.ShoppingCartItem
                        .Where(m => m.ShoppingCartId == shopCartId)
                         select t).ToList();

            for (var i = 0; i < items.Count(); i++)
            {
                var item = new ShopCartItemVM()
                {
                    Id = items[i].Id,
                    Amount = items[i].Amount,

                    Product = new ProductService().FindProduct(items[i].ProductId.Value),
                    Price = items[i].Price,
                };
                itemsVMs.Add(item);

            }
            return itemsVMs;
        }
    }
}
