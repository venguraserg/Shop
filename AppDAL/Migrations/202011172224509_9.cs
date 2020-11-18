namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCartItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        ProductId = c.Guid(),
                        ShoppingCartId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCartId)
                .Index(t => t.ProductId)
                .Index(t => t.ShoppingCartId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCartItem", "ShoppingCartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.ShoppingCartItem", "ProductId", "dbo.Product");
            DropIndex("dbo.ShoppingCartItem", new[] { "ShoppingCartId" });
            DropIndex("dbo.ShoppingCartItem", new[] { "ProductId" });
            DropTable("dbo.ShoppingCartItem");
        }
    }
}
