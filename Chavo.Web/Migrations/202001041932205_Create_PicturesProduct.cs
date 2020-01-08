namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_PicturesProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PicturesProducts",
                c => new
                    {
                        PicturesProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PicturesProductId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PicturesProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.PicturesProducts", new[] { "ProductId" });
            DropTable("dbo.PicturesProducts");
        }
    }
}
