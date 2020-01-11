namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_DataBase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "DisplayShortRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayMediumRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayLongRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayClothes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayInversor", c => c.Boolean(nullable: false));
            AddColumn("dbo.GeneralConfigurations", "AcquireUserLearningZone", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "TransacctionNumber", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "DescriptionTransacctionNumber", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "MessageLogin", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "MessageFooter", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GeneralConfigurations", "MessageFooter");
            DropColumn("dbo.GeneralConfigurations", "MessageLogin");
            DropColumn("dbo.GeneralConfigurations", "DescriptionTransacctionNumber");
            DropColumn("dbo.GeneralConfigurations", "TransacctionNumber");
            DropColumn("dbo.GeneralConfigurations", "AcquireUserLearningZone");
            DropColumn("dbo.Customers", "DisplayInversor");
            DropColumn("dbo.Customers", "DisplayClothes");
            DropColumn("dbo.Customers", "DisplayLongRevenue");
            DropColumn("dbo.Customers", "DisplayMediumRevenue");
            DropColumn("dbo.Customers", "DisplayShortRevenue");
            DropColumn("dbo.Products", "Quantity");
        }
    }
}
