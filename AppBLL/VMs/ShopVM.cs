using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.VMs
{
    public class ShopVM : BaseVMmodel
    {
        //public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"\tName: {Name},\t\t Description: {Description}";
        }
    }
}
