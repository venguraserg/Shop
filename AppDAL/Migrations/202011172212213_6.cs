namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShoppingCart", "Capacity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "Capacity", c => c.Int(nullable: false));
        }
    }
}
