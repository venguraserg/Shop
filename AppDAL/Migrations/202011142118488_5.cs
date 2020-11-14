namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Category", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Category", c => c.Int(nullable: false));
        }
    }
}
