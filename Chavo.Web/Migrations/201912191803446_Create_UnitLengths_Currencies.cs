namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_UnitLengths_Currencies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nomenclature = c.String(),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.UnitLengths",
                c => new
                    {
                        UnitLengthId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Nomenclature = c.String(),
                    })
                .PrimaryKey(t => t.UnitLengthId);
            
            AddColumn("dbo.Products", "CurrencyId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "UnitLengthId", c => c.Int());
            CreateIndex("dbo.Products", "CurrencyId");
            CreateIndex("dbo.Products", "UnitLengthId");
            AddForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "UnitLengthId", "dbo.UnitLengths", "UnitLengthId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UnitLengthId", "dbo.UnitLengths");
            DropForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies");
            DropIndex("dbo.Products", new[] { "UnitLengthId" });
            DropIndex("dbo.Products", new[] { "CurrencyId" });
            DropColumn("dbo.Products", "UnitLengthId");
            DropColumn("dbo.Products", "CurrencyId");
            DropTable("dbo.UnitLengths");
            DropTable("dbo.Currencies");
        }
    }
}
