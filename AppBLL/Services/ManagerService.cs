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
    public class ManagerService : IManagerService
    {
        private MyShopDbContext db { get; set; }
        public ManagerService()
        {
            db = new MyShopDbContext();
        }
        public void AddManager(string login, string passHash, string name, string surname, string phonenumber, Guid shopId)
        {
            var new_manager = new Manager()
            {
                Login = login,
                PasswordHash = passHash,
                Name = name,
                Surname = surname,
                PhoneNumber = phonenumber,
                ShopId = shopId

            };
            db.Manager.Add(new_manager);
            db.SaveChanges();
        }


        public bool LogInManager(string username, string passwordHash)
        {
            var user = db.Manager.Where(m => m.Login.Equals(username) && m.PasswordHash.Equals(passwordHash));
            if (user == null || user.Count() != 1) { return false; }
            UserVM.Id = user.Single().Id;
            UserVM.Role = user.Single().Role;
            return true;
        }

        public List<ManagerVM> GetAllManagersInfo()
        {
            List<ManagerVM> managerVMs = new List<ManagerVM>();

            var managers = (from t in db.Manager
                         select t).ToList();

            for (var i = 0; i < managers.Count(); i++)
            {
                var item = new ManagerVM() 
                { 
                    Id = managers[i].Id, 
                    Login = managers[i].Login, 
                    Name = managers[i].Name, 
                    Surname = managers[i].Surname, 
                    PhoneNumber = managers[i].PhoneNumber, 
                    Shop = new ShopService().FindShop((Guid)managers[i].ShopId) 
                };
                managerVMs.Add(item);

            }
            return managerVMs;
        }

        public int GetNumbOfItem()
        {
            return db.Manager.Count();
        }

        public List<ManagerVM> GetPageManagerInfo(int start_items, int amount_items)
        {
            List<ManagerVM> managersVMs = new List<ManagerVM>();

            var manager = db.Manager.OrderBy(p => p.Id)
                                    .Skip(start_items)
                                    .Take(amount_items)
                                    .ToList();

            for (var i = 0; i < manager.Count(); i++)
            {
                var item = new ManagerVM()
                {
                    Id = manager[i].Id,
                    Login = manager[i].Login,
                    Name = manager[i].Name,
                    Surname = manager[i].Surname,
                    PhoneNumber = manager[i].PhoneNumber,
                    Shop = new ShopService().FindShop((Guid)manager[i].ShopId)

                };
                managersVMs.Add(item);

            }
            return managersVMs;

        }
        public void DeleteManager(Guid id)
        {
            var manager = db.Manager.Find(id);
            if (manager == null) { return; }
            db.Manager.Remove(manager);
            db.SaveChanges();
        }

        
    }
}
