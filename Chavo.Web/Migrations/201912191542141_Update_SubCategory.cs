namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SubCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubCategories", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "Category_CategoryId" });
            RenameColumn(table: "dbo.SubCategories", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.SubCategories", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubCategories", "CategoryId");
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            AlterColumn("dbo.SubCategories", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.SubCategories", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.SubCategories", "Category_CategoryId");
            AddForeignKey("dbo.SubCategories", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
