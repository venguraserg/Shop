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

        public int GetNumbOfItem()
        {
            return db.Unit.Count();
        }

        public List<UnitVM> GetPageUnitInfo(int start_items, int amount_items)
        {
            List<UnitVM> unitVMs = new List<UnitVM>();

            var unit = db.Unit.OrderBy(p => p.Id)
                               .Skip(start_items)
                               .Take(amount_items)
                               .ToList();

            for (var i = 0; i < unit.Count(); i++)
            {
                var item = new UnitVM() { Id = unit[i].Id, Name = unit[i].Name};
                unitVMs.Add(item);

            }
            return unitVMs;

        }

    }
}
