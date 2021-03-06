﻿using App_Model.Models;
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
        public bool DeleteBuyer(Guid id)
        {
            var buyer = db.Buyer.Find(id);
            if (buyer == null) { return false; }
            db.Buyer.Remove(buyer);
            db.SaveChanges();
            return true;
        }

        public bool LogInBuyer(string username, string passwordHash)
        {
            var user = db.Buyer.Where(m => m.Login.Equals(username) && m.PasswordHash.Equals(passwordHash));
            if (user == null || user.Count() != 1) { return false; }
            UserVM.Id = user.Single().Id;
            UserVM.Role = user.Single().Role;
            return true;

        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="phonenumber"></param>
        /// <param name="address"></param>
        /// <param name="dateofbirth"></param>
        /// <param name="mode">после рестрации входим под новым пользователем True - да/False - нет</param>
        public void RegisterNewBuyer(string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth, bool mode)
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
            if (mode)
            {
                UserVM.Id = new_buyer.Id;
                UserVM.Role = new_buyer.Role;
            }
            



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
        public List<BuyerVM> GetPageInfo(int start_items, int amount_items)
        {
            List<BuyerVM> buyerVMs = new List<BuyerVM>();

            var buyer = db.Buyer.OrderBy(p => p.Login)
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
        /// <summary>
        /// Метод подсчета количества строк в таблице
        /// </summary>
        /// <returns>
        /// возращает int [количество строк]
        /// </returns>
        public int GetNumbOfItem()
        {
            return db.Buyer.Count();
        }

        /// <summary>
        /// Поиск совпадения по Логину. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>TRUE - Совпадение</returns>
        public bool SearchBuyer(string username)
        {
            var loginList = (from t in db.Buyer
                            select t.Login).ToList();
            foreach (var i in loginList) 
            {
                if (i == username) { return true; }
            
            }
            return false; 
            
        }

        public void UpdateBuyer(Guid id, string username, string passwordHash, string name, string surname, string phonenumber, string address, DateTime dateofbirth)
        {
            var buyer = db.Buyer.Find(id);
            buyer.Login = username;
            buyer.PasswordHash = passwordHash;
            buyer.Name = name;
            buyer.Surname = surname;
            buyer.PhoneNumber = phonenumber;
            buyer.Address = address;
            buyer.DateOfBirth = dateofbirth;
            db.SaveChanges();

        }

        public BuyerVM GetBuyer(Guid Id)
        {
            var buyer = db.Buyer.Find(Id);
            if (buyer == null) { return null; }
            var item = new BuyerVM()
            {
                Id = buyer.Id,
                Login = buyer.Login,
                Name = buyer.Name,
                Surname = buyer.Surname,
                PhoneNumber = buyer.PhoneNumber,
                Address = buyer.Address,
                DateOfBirth = buyer.DateOfBirth,
                DateOfRegister = buyer.DateOfRegister

            };
            return item;
        }
    }
}
