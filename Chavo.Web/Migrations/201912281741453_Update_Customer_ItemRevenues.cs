namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Customer_ItemRevenues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ShortRevenue", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "MediumRevenue", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "LongRevenue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "LongRevenue");
            DropColumn("dbo.Customers", "MediumRevenue");
            DropColumn("dbo.Customers", "ShortRevenue");
        }
    }
}
