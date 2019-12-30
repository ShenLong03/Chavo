namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Products : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ShortDescription", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ShortDescription", c => c.String(maxLength: 50));
        }
    }
}
