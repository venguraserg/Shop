namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Product", "UnitId", "dbo.Unit");
            DropIndex("dbo.Product", new[] { "ShopId" });
            DropIndex("dbo.Product", new[] { "UnitId" });
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percent = c.Double(nullable: false),
                        DateStart = c.DateTime(),
                        DateEnd = c.DateTime(),
                        Text = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "DiscountId", c => c.Guid());
            AlterColumn("dbo.Product", "ShopId", c => c.Guid());
            AlterColumn("dbo.Product", "UnitId", c => c.Guid());
            CreateIndex("dbo.Product", "ShopId");
            CreateIndex("dbo.Product", "UnitId");
            CreateIndex("dbo.Product", "DiscountId");
            AddForeignKey("dbo.Product", "DiscountId", "dbo.Discount", "Id");
            AddForeignKey("dbo.Product", "ShopId", "dbo.Shop", "Id");
            AddForeignKey("dbo.Product", "UnitId", "dbo.Unit", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "UnitId", "dbo.Unit");
            DropForeignKey("dbo.Product", "ShopId", "dbo.Shop");
            DropForeignKey("dbo.Product", "DiscountId", "dbo.Discount");
            DropIndex("dbo.Product", new[] { "DiscountId" });
            DropIndex("dbo.Product", new[] { "UnitId" });
            DropIndex("dbo.Product", new[] { "ShopId" });
            AlterColumn("dbo.Product", "UnitId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Product", "ShopId", c => c.Guid(nullable: false));
            DropColumn("dbo.Product", "DiscountId");
            DropColumn("dbo.Product", "Category");
            DropTable("dbo.Discount");
            CreateIndex("dbo.Product", "UnitId");
            CreateIndex("dbo.Product", "ShopId");
            AddForeignKey("dbo.Product", "UnitId", "dbo.Unit", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Product", "ShopId", "dbo.Shop", "Id", cascadeDelete: true);
        }
    }
}
