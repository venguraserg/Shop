namespace AppDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shop", "Discription2", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shop", "Discription2");
        }
    }
}
