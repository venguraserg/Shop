namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "BuyerId", "dbo.Buyer");
            DropForeignKey("dbo.Buyer", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Product", "UnitId", "dbo.Unit");
            DropForeignKey("dbo.Manager", "ShopId", "dbo.Shop");
            DropIndex("dbo.Buyer", new[] { "ShoppingCartId" });
            DropIndex("dbo.Order", new[] { "BuyerId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "ShopId" });
            DropIndex("dbo.Product", new[] { "UnitId" });
            DropIndex("dbo.Manager", new[] { "ShopId" });
            AlterColumn("dbo.Buyer", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Buyer", "DateOfRegister", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Buyer", "ShoppingCartId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Order", "BuyerId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Order", "ProductId", c => c.Guid(nullable: false));
            AlterColumn("dbo.OrderItem", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.Product", "Amount", c => c.Single(nullable: false));
            AlterColumn("dbo.Product", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 0));
            AlterColumn("dbo.Product", "ShopId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Product", "UnitId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Manager", "ShopId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ShoppingCart", "Capacity", c => c.Int(nullable: false));
            CreateIndex("dbo.Buyer", "ShoppingCartId");
            CreateIndex("dbo.Order", "BuyerId");
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Product", "ShopId");
            CreateIndex("dbo.Product", "UnitId");
            CreateIndex("dbo.Manager", "ShopId");
            AddForeignKey("dbo.Order", "BuyerId", "dbo.Buyer", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Buyer", "ShoppingCartId", "dbo.ShoppingCart", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "ShopId", "dbo.Shop", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "UnitId", "dbo.Unit", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Manager", "ShopId", "dbo.Shop", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manager", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Product", "UnitId", "dbo.Unit");
            DropForeignKey("dbo.Product", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Order", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Buyer", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.Order", "BuyerId", "dbo.Buyer");
            DropIndex("dbo.Manager", new[] { "ShopId" });
            DropIndex("dbo.Product", new[] { "UnitId" });
            DropIndex("dbo.Product", new[] { "ShopId" });
            DropIndex("dbo.Order", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "BuyerId" });
            DropIndex("dbo.Buyer", new[] { "ShoppingCartId" });
            AlterColumn("dbo.ShoppingCart", "Capacity", c => c.Int());
            AlterColumn("dbo.Manager", "ShopId", c => c.Guid());
            AlterColumn("dbo.Product", "UnitId", c => c.Guid());
            AlterColumn("dbo.Product", "ShopId", c => c.Guid());
            AlterColumn("dbo.Product", "Price", c => c.Decimal(precision: 18, scale: 0));
            AlterColumn("dbo.Product", "Amount", c => c.Single());
            AlterColumn("dbo.OrderItem", "Count", c => c.Int());
            AlterColumn("dbo.Order", "ProductId", c => c.Guid());
            AlterColumn("dbo.Order", "BuyerId", c => c.Guid());
            AlterColumn("dbo.Buyer", "ShoppingCartId", c => c.Guid());
            AlterColumn("dbo.Buyer", "DateOfRegister", c => c.DateTime());
            AlterColumn("dbo.Buyer", "DateOfBirth", c => c.DateTime());
            CreateIndex("dbo.Manager", "ShopId");
            CreateIndex("dbo.Product", "UnitId");
            CreateIndex("dbo.Product", "ShopId");
            CreateIndex("dbo.Order", "ProductId");
            CreateIndex("dbo.Order", "BuyerId");
            CreateIndex("dbo.Buyer", "ShoppingCartId");
            AddForeignKey("dbo.Manager", "ShopId", "dbo.Shop", "Id");
            AddForeignKey("dbo.Product", "UnitId", "dbo.Unit", "Id");
            AddForeignKey("dbo.Product", "ShopId", "dbo.Shop", "Id");
            AddForeignKey("dbo.Order", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Buyer", "ShoppingCartId", "dbo.ShoppingCart", "Id");
            AddForeignKey("dbo.Order", "BuyerId", "dbo.Buyer", "Id");
        }
    }
}
