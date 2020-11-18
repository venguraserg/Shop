namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCartItem", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.ShoppingCartItem", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCartItem", "Price");
            DropColumn("dbo.ShoppingCartItem", "Amount");
        }
    }
}
