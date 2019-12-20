namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Products", "Nombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Nombre", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Products", "Name");
        }
    }
}
