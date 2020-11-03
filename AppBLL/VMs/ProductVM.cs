using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.VMs
{
    public class ProductVM : BaseVMmodel
    {
       // public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public decimal Price { get; set; }

        //public Guid? ShopId { get; set; }

        //public Guid? UnitId { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
