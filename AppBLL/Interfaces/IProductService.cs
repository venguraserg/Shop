using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IProductService
    {
        ///Create
        void AddProduct(string name, string description, float amount, decimal price, Guid shopId, Guid unitId);
        int GetNumbOfItem();
        List<ProductVM> GetPageInfo(int start_items, int amount_items);
        ProductVM GetProduct(Guid Id);
        void UpdateProduct(Guid id, string name, string description, float amount, decimal price, Guid shopId, Guid unitId);
        bool DeleteProduct(Guid Id);
    }
}
