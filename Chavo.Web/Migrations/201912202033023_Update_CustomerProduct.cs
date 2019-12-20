namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CustomerProduct : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CustomerProducts", "CustomerId");
            CreateIndex("dbo.CustomerProducts", "ProductId");
            AddForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerProducts", new[] { "ProductId" });
            DropIndex("dbo.CustomerProducts", new[] { "CustomerId" });
        }
    }
}
