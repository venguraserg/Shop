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
        //Read
        bool LogInManager(string username, string passwordHash);
        List<ManagerVM> GetAllManagersInfo();
        //Update
        //Delete

    }
}
