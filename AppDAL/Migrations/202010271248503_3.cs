namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(maxLength: 50),
                        PaswordHash = c.String(maxLength: 50),
                        ShopId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shop", t => t.ShopId)
                .Index(t => t.ShopId);
            
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Discription = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manager", "ShopId", "dbo.Shop");
            DropIndex("dbo.Manager", new[] { "ShopId" });
            DropTable("dbo.Shop");
            DropTable("dbo.Manager");
            DropTable("dbo.Buyer");
        }
    }
}
