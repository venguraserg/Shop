using App_Model.Models;
using AppBLL.Interfaces;
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
    }
}
