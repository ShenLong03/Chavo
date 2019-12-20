namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        Code = c.String(nullable: false, maxLength: 200),
                        Serie = c.String(),
                        Model = c.String(),
                        Age = c.String(),
                        weight = c.Double(nullable: false),
                        PesoId = c.Int(nullable: false),
                        Width = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                        Picture = c.String(),
                        Description = c.String(),
                        CostAmount = c.Double(nullable: false),
                        PriceAmount = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Video = c.String(),
                        SubCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.Code, unique: true, name: "Product_Code_Index")
                .Index(t => t.SubCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            DropIndex("dbo.Products", "Product_Code_Index");
            DropTable("dbo.Products");
        }
    }
}
