namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Categories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Picture = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.Name, unique: true, name: "Category_Name_Index");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", "Category_Name_Index");
            DropTable("dbo.Categories");
        }
    }
}
