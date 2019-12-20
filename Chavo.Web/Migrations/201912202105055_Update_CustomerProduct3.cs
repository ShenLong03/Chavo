namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CustomerProduct3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CustomerProductViewModel_CustomerProductId", "dbo.CustomerProducts");
            DropIndex("dbo.Products", new[] { "CustomerProductViewModel_CustomerProductId" });
            DropColumn("dbo.Products", "CustomerProductViewModel_CustomerProductId");
            DropColumn("dbo.CustomerProducts", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerProducts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Products", "CustomerProductViewModel_CustomerProductId", c => c.Int());
            CreateIndex("dbo.Products", "CustomerProductViewModel_CustomerProductId");
            AddForeignKey("dbo.Products", "CustomerProductViewModel_CustomerProductId", "dbo.CustomerProducts", "CustomerProductId");
        }
    }
}
