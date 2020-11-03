using App_Model.Models;
using AppBLL.Interfaces;
using AppDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Services
{
    public class UnitService : IUnitService
        
    {
        private MyShopDbContext db { get; set; }
        public UnitService()
        {
            db = new MyShopDbContext();
        }

        public void AddUnit(string name)
        {
            var new_unit = new Unit() { Name = name };
            db.Unit.Add(new_unit);
            db.SaveChanges();
        }
    }
}
