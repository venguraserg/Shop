﻿using AppBLL.Interfaces;
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
    }
}
