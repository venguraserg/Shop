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
        List<ManagerVM> GetPageInfo(int start_items, int amount_items);
        List<ManagerVM> GetAllManagersInfo();
        ManagerVM GetManager(Guid Id);

        //Update
        void UpdateManager(Guid id, string login, string passHash, string name, string surname, string phonenumber, Guid shopId);

        //Delete
        bool DeleteManager(Guid id);


        //Service functions
        int GetNumbOfItem();

    }
}
