using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IManagerService
    {
        //Create
        void AddManager(string login, string passHash, string name, string surname, string phonenumber, Guid shopId);
        //Read
        bool LogInManager(string username, string passwordHash);
        bool SearchManager(string username);
        List<ManagerVM> GetPageManagerInfo(int start_items, int amount_items);
        List<ManagerVM> GetAllManagersInfo();
        //Update
        //Delete
        void DeleteManager(Guid id);
        int GetNumbOfItem();

    }
}
