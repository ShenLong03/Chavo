namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_SubCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Picture = c.String(),
                        Active = c.Boolean(nullable: false),
                        Category_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "Category_CategoryId" });
            DropTable("dbo.SubCategories");
        }
    }
}
