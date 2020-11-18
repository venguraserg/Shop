namespace App_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        
        public Order()
        {
            Id = Guid.NewGuid();
            OrderItem = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }

        public DateTime? DateOfOrder { get; set; }

        public Guid BuyerId { get; set; }

        public Guid ProductId { get; set; }

        public virtual Buyer Buyer { get; set; }

        public virtual Product Product { get; set; }

        
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
