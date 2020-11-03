namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Role = c.String(maxLength: 15),
                        Login = c.String(maxLength: 20),
                        PasswordHash = c.String(maxLength: 20),
                        Name = c.String(maxLength: 20),
                        Surname = c.String(maxLength: 20),
                        PhoneNumber = c.String(maxLength: 20),
                        Address = c.String(maxLength: 256),
                        DateOfBirth = c.DateTime(),
                        DateOfRegister = c.DateTime(),
                        ShoppingCartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCartId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfOrder = c.DateTime(),
                        BuyerId = c.Guid(),
                        ProductId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyer", t => t.BuyerId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.BuyerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Count = c.Int(),
                        OrderId = c.Guid(),
                        ProductId = c.Guid(),
                        ShoppingCartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCartId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCartId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 200),
                        Amount = c.Single(),
                        Price = c.Decimal(precision: 18, scale: 0),
                        ShopId = c.Guid(),
                        UnitId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .ForeignKey("dbo.Unit", t => t.UnitId)
                .Index(t => t.ShopId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Discription = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Role = c.String(maxLength: 15),
                        Login = c.String(maxLength: 20),
                        PasswordHash = c.String(maxLength: 20),
                        Name = c.String(maxLength: 20),
                        Surname = c.String(maxLength: 20),
                        PhoneNumber = c.String(maxLength: 20),
                        ShopId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.Unit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Unit = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Capacity = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.Buyer", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.Product", "UnitId", "dbo.Unit");
            DropForeignKey("dbo.Product", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Manager", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "BuyerId", "dbo.Buyer");
            DropIndex("dbo.Manager", new[] { "ShopId" });
            DropIndex("dbo.Product", new[] { "UnitId" });
            DropIndex("dbo.Product", new[] { "ShopId" });
            DropIndex("dbo.OrderItem", new[] { "ShoppingCartId" });
            DropIndex("dbo.OrderItem", new[] { "ProductId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "BuyerId" });
            DropIndex("dbo.Buyer", new[] { "ShoppingCartId" });
            DropTable("dbo.ShoppingCart");
            DropTable("dbo.Unit");
            DropTable("dbo.Manager");
            DropTable("dbo.Shop");
            DropTable("dbo.Product");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
            DropTable("dbo.Buyer");
        }
    }
}
