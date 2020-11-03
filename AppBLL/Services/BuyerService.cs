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
    public class BuyerService : IBuyerService
    {
        private MyShopDbContext db { get; set; }
        public BuyerService()
        {
            db = new MyShopDbContext();
        }
        public void DeleteBuyer(Guid id)
        {
            var buyer = db.Buyer.Find(id);
            if (buyer == null) { return; }
            db.Buyer.Remove(buyer);
            db.SaveChanges();
        }

        public bool LogInBuyer(string username, string passwordHash)
        {
            var user = db.Buyer.Where(m => m.Login.Equals(username) && m.PasswordHash.Equals(passwordHash));
            if (user == null || user.Count() != 1) { return false; }
            UserVM.Id = user.Single().Id;
            UserVM.Role = user.Single().Role;
            return true;

        }

        public void RegisterNewBuyer(string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth)
        {
            ShopCartService sc = new ShopCartService();

            Buyer new_buyer = new Buyer()
            {
                Login = username,
                PasswordHash = passwordHash,
                Name = name,
                Surname = surname,
                PhoneNumber = phonenumber,
                Address = address,
                DateOfBirth = dateofbirth,
                DateOfRegister = DateTime.Now,
                ShoppingCartId = sc.CreateShopCart()
            };
            db.Buyer.Add(new_buyer);
            db.SaveChanges();
            UserVM.Id = new_buyer.Id;
            UserVM.Role = new_buyer.Role;



        }

        public List<BuyerVM> GetAllBuyersInfo()
        {
            List<BuyerVM> buyerVMs = new List<BuyerVM>();

            var buyer = (from t in db.Buyer
                            select t).ToList();

            for (var i = 0; i < buyer.Count(); i++)
            {
                var item = new BuyerVM()
                {
                    Id = buyer[i].Id,
                    Login = buyer[i].Login,
                    Name = buyer[i].Name,
                    Surname = buyer[i].Surname,
                    PhoneNumber = buyer[i].PhoneNumber,
                    Address = buyer[i].Address,
                    DateOfBirth = (DateTime)buyer[i].DateOfBirth,
                    DateOfRegister = (DateTime)buyer[i].DateOfRegister

                };
                buyerVMs.Add(item);

            }
            return buyerVMs;
        }
        public List<BuyerVM> GetPageBuyerInfo(int start_items, int amount_items)
        {
            List<BuyerVM> buyerVMs = new List<BuyerVM>();

            var buyer = db.Buyer.OrderBy(p => p.Id)
                               .Skip(start_items)
                               .Take(amount_items)
                               .ToList();

            for (var i = 0; i < buyer.Count(); i++)
            {
                var item = new BuyerVM()
                {
                    Id = buyer[i].Id,
                    Login = buyer[i].Login,
                    Name = buyer[i].Name,
                    Surname = buyer[i].Surname,
                    PhoneNumber = buyer[i].PhoneNumber,
                    Address = buyer[i].Address,
                    DateOfBirth = (DateTime)buyer[i].DateOfBirth,
                    DateOfRegister = (DateTime)buyer[i].DateOfRegister

                };
                buyerVMs.Add(item);

            }
            return buyerVMs;

        }
        public int GetNumbOfItem()
        {
            return db.Buyer.Count();
        }
    }
}
