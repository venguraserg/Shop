namespace App_Model.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("OrderItem")]
    public partial class OrderItem
    {
        public OrderItem()
        {

            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int Count { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? ShoppingCartId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
