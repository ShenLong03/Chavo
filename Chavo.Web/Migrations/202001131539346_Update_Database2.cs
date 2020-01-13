namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Unique", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "RealState", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "MaxToWin", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "Sizes", c => c.String());
            AddColumn("dbo.Customers", "Comentaries", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "SelectClothes", c => c.String());
            DropColumn("dbo.Products", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.GeneralConfigurations", "SelectClothes");
            DropColumn("dbo.Customers", "Comentaries");
            DropColumn("dbo.Customers", "Sizes");
            DropColumn("dbo.Customers", "MaxToWin");
            DropColumn("dbo.Products", "RealState");
            DropColumn("dbo.Products", "Unique");
        }
    }
}
