using App_Model.Models;
using AppBLL.Interfaces;
using AppBLL.VMs;
using AppDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Services
{
    public class ShopService : IShopService
    {
        private MyShopDbContext db { get; set; }
        public ShopService()
        {
            db = new MyShopDbContext();
        }
        public bool DeleteShop(Guid id)
        {
            var user = db.Shop.Find(id);
            if (user == null) { return false; }
            db.Shop.Remove(user);
            db.SaveChanges();
            return true;
        }

        public void RegisterNewShop(string shopname, string discription)
        {
            var newshop = new Shop() { Name = shopname, Discription = discription };
            db.Shop.Add(newshop);
            db.SaveChanges();

        }

        
        public string FindShop(Guid Id)
        {
            var shop = db.Shop.Find(Id);
            if (shop == null)
                return "no data";
            else
                return shop.Name;
        }
        public int GetNumbOfItemShop()
        {
            return db.Shop.Count();
        }
        public List<ShopVM> GetPageShopInfo(int start_items, int amount_items)
        {
            List<ShopVM> shopVMs = new List<ShopVM>();

            var shops = db.Shop.OrderBy(p => p.Id)
                               .Skip(start_items)
                               .Take(amount_items)
                               .ToList();

            for (var i = 0; i < shops.Count(); i++)
            {
                var item = new ShopVM() { Id = shops[i].Id, Name = shops[i].Name, Description = shops[i].Discription };
                shopVMs.Add(item);

            }
            return shopVMs;

        }


        public void UpdateShop()
        {
            throw new NotImplementedException();
        }
    }
}
