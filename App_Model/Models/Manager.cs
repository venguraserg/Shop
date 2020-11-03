namespace App_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manager")]
    public partial class Manager
    {
       
        public Manager()
        {
            Id = Guid.NewGuid();
            Role = "manager";
        }
        
        public Guid Id { get; set; }

        [StringLength(15)]
        public string Role { get; set; }

        [StringLength(20)]
        public string Login { get; set; }

        [StringLength(20)]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Surname { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public Guid? ShopId { get; set; }

        public virtual Shop Shop { get; set; }


        public override string ToString()
        {
            return $"Имя   {Name}, Роль    {Role}";
        }
    }
}
