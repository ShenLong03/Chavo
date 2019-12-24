namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_GeneralConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneralConfigurations",
                c => new
                    {
                        GeneralConfigurationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        VideoBanner = c.String(),
                    })
                .PrimaryKey(t => t.GeneralConfigurationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeneralConfigurations");
        }
    }
}
