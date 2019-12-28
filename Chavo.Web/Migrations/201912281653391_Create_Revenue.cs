namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Revenue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Revenues",
                c => new
                    {
                        RevenueId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        RevenueType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        FunctionaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RevenueId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Functionaries", t => t.FunctionaryId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.FunctionaryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Revenues", "FunctionaryId", "dbo.Functionaries");
            DropForeignKey("dbo.Revenues", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Revenues", new[] { "FunctionaryId" });
            DropIndex("dbo.Revenues", new[] { "CustomerId" });
            DropTable("dbo.Revenues");
        }
    }
}
