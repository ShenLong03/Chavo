namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product_ShortDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShortDescription", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShortDescription");
        }
    }
}
