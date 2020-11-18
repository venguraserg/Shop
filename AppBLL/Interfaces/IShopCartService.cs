using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IShopCartService
    {

        //Create
        Guid CreateShopCart();
        //Read
        Guid GetShopCart(Guid userId);
        List<ShopCartItemVM> GetAllItems(Guid shopCartId);
        int GetNumbOfItem(Guid shopCartId);
        List<ShopCartItemVM> GetPageInfo(int start_items, int amount_items, Guid shopCartId);
        //Update
        //Delete

        void AddInShopCart(Guid shopCartId,Guid productId, float amount, decimal price);

    }
}
