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
        void RegisterNewBuyer(string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth, bool mode);
        bool LogInBuyer(string username, string passwordHash);
        bool SearchBuyer(string username);
        List<BuyerVM> GetAllBuyersInfo();
        List<BuyerVM> GetPageInfo(int start_items, int amount_items);
        BuyerVM GetBuyer(Guid Id);

        //Update
        void UpdateBuyer(Guid id, string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth);

        
        bool DeleteBuyer(Guid id);


        int GetNumbOfItem();
    }
}
