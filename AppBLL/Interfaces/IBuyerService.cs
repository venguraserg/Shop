using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IBuyerService
    {
        void RegisterNewBuyer(string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth);
        bool LogInBuyer(string username, string passwordHash);
        List<BuyerVM> GetAllBuyersInfo();
        List<BuyerVM> GetPageBuyerInfo(int start_items, int amount_items);
        int GetNumbOfItem();
        void DeleteBuyer(Guid id);
    }
}
