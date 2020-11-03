using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBLL.VMs
{
    public class ManagerVM : BaseVMmodel
    {
       // public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Shop { get; set; }

        public override string ToString()
        {
            return $"Login: {Login},\t Name: {Name},\t Surname: {Surname},\t PhoneNunber: {PhoneNumber},\t Shop: {Shop}";
        }

    }
}
