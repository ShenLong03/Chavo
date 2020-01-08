namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_DataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PicturesProducts", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PicturesProducts", "Picture");
        }
    }
}
