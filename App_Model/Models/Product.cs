namespace App_Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Id = Guid.NewGuid();
            Order = new HashSet<Order>();
            OrderItem = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public float Amount { get; set; }

        public decimal Price { get; set; }

        
        public Category? Category { get; set; }

        public Guid? ShopId { get; set; }

        public Guid? UnitId { get; set; }

        public Guid? DiscountId { get; set; }

        
        public virtual ICollection<Order> Order { get; set; }

        
        public virtual ICollection<OrderItem> OrderItem { get; set; }

        public virtual Shop Shop { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Discount Discount { get; set; }





    }
    public enum Category 
    {
        food = 1, //food
        gadget,     //gadget
        category_3,
        category_4

    }

}
