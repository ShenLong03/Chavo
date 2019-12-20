namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Weight : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weights",
                c => new
                    {
                        WeightId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nomenclature = c.String(),
                    })
                .PrimaryKey(t => t.WeightId);
            
            AddColumn("dbo.Products", "WeightId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "WeightId");
            AddForeignKey("dbo.Products", "WeightId", "dbo.Weights", "WeightId", cascadeDelete: true);
            DropColumn("dbo.Products", "PesoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "PesoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Products", "WeightId", "dbo.Weights");
            DropIndex("dbo.Products", new[] { "WeightId" });
            DropColumn("dbo.Products", "WeightId");
            DropTable("dbo.Weights");
        }
    }
}
