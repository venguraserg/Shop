namespace App_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Buyer")]
    public partial class Buyer
    {
        
        public Buyer()
        {
            Id = Guid.NewGuid();
            Order = new HashSet<Order>();
            Role = "buyer";
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

        [StringLength(256)]
        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfRegister { get; set; }

        public Guid? ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
