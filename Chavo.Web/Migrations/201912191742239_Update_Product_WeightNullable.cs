namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_WeightNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "WeightId", "dbo.Weights");
            DropIndex("dbo.Products", new[] { "WeightId" });
            AlterColumn("dbo.Products", "WeightId", c => c.Int());
            CreateIndex("dbo.Products", "WeightId");
            AddForeignKey("dbo.Products", "WeightId", "dbo.Weights", "WeightId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "WeightId", "dbo.Weights");
            DropIndex("dbo.Products", new[] { "WeightId" });
            AlterColumn("dbo.Products", "WeightId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "WeightId");
            AddForeignKey("dbo.Products", "WeightId", "dbo.Weights", "WeightId", cascadeDelete: true);
        }
    }
}
