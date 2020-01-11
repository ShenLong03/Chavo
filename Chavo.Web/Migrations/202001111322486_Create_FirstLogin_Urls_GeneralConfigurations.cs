namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_FirstLogin_Urls_GeneralConfigurations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FirstLogins",
                c => new
                    {
                        FirstLoginId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FirstLoginId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Products", "AcquireProduct", c => c.String());
            AddColumn("dbo.Products", "State", c => c.Int(nullable: false));
            AddColumn("dbo.GeneralConfigurations", "Email", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Phone", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Facebook", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "FacebookGroup", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Instagram", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Twiter", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "LearningZone", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Cashing", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "CashingConditions", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "RealEstate", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "Chat", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FirstLogins", "CustomerId", "dbo.Customers");
            DropIndex("dbo.FirstLogins", new[] { "CustomerId" });
            DropColumn("dbo.GeneralConfigurations", "Chat");
            DropColumn("dbo.GeneralConfigurations", "RealEstate");
            DropColumn("dbo.GeneralConfigurations", "CashingConditions");
            DropColumn("dbo.GeneralConfigurations", "Cashing");
            DropColumn("dbo.GeneralConfigurations", "LearningZone");
            DropColumn("dbo.GeneralConfigurations", "Twiter");
            DropColumn("dbo.GeneralConfigurations", "Instagram");
            DropColumn("dbo.GeneralConfigurations", "FacebookGroup");
            DropColumn("dbo.GeneralConfigurations", "Facebook");
            DropColumn("dbo.GeneralConfigurations", "Phone");
            DropColumn("dbo.GeneralConfigurations", "Email");
            DropColumn("dbo.Products", "State");
            DropColumn("dbo.Products", "AcquireProduct");
            DropTable("dbo.FirstLogins");
        }
    }
}
