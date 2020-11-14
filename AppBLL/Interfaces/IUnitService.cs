using AppBLL.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.Interfaces
{
    public interface IUnitService
    {
        void AddUnit(string Name);
        int GetNumbOfItem();
        List<UnitVM> GetPageInfo(int start_items, int amount_items);
        UnitVM GetUnit(Guid Id);
        void UpdateUnit(Guid id, string name);
    }

}
