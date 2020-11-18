using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.VMs
{
    public class ShopCartItemVM : BaseVMmodel
    {
        public string Product { get; set; }
        public float Amount { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Product},\t Цена: {Price},\t Количество {Amount}";

        }

    }
}
