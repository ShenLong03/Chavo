namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_GeneralConfiguration_VideoCashing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeneralConfigurations", "VideoCashing", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GeneralConfigurations", "VideoCashing");
        }
    }
}
