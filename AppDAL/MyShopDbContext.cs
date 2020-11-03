using App_Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDAL
{
    public partial class MyShopDbContext : DbContext
    {
        public MyShopDbContext()
            : base("MyShopDB")
        {
        }

        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}
