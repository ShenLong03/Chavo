namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_CustomerProduct2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CustomerProductViewModel_CustomerProductId", c => c.Int());
            AddColumn("dbo.CustomerProducts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Products", "CustomerProductViewModel_CustomerProductId");
            AddForeignKey("dbo.Products", "CustomerProductViewModel_CustomerProductId", "dbo.CustomerProducts", "CustomerProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CustomerProductViewModel_CustomerProductId", "dbo.CustomerProducts");
            DropIndex("dbo.Products", new[] { "CustomerProductViewModel_CustomerProductId" });
            DropColumn("dbo.CustomerProducts", "Discriminator");
            DropColumn("dbo.Products", "CustomerProductViewModel_CustomerProductId");
        }
    }
}
