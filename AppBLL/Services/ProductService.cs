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
    public class ProductService : IProductService
    {
        private MyShopDbContext db { get; set; }
        public ProductService()
        {
            db = new MyShopDbContext();
        }
        public void AddProduct(string name, string description, float amount, decimal price, Guid shopId, Guid unitId)
        {
            var new_product = new Product()
            {
                Name = name,
                Description = description,
                Amount = amount,
                Price = price,
                ShopId = shopId,
                UnitId = unitId
            };
            db.Product.Add(new_product);
            db.SaveChanges();
        }
        public int GetNumbOfItem()
        {
            return db.Product.Count();
        }

        public List<ProductVM> GetPageProductInfo(int start_items, int amount_items)
        {
            List<ProductVM> prodVMs = new List<ProductVM>();

            var unit = db.Product.OrderBy(p => p.Id)
                               .Skip(start_items)
                               .Take(amount_items)
                               .ToList();

            for (var i = 0; i < unit.Count(); i++)
            {
                var item = new ProductVM() { Id = unit[i].Id, Name = unit[i].Name };
                prodVMs.Add(item);

            }
            return prodVMs;
        }
    }
}
