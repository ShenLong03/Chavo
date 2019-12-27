namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Register : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registers",
                c => new
                    {
                        RegisterId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Transaction = c.String(),
                        Password = c.String(),
                        Approved = c.Boolean(nullable: false),
                        ChangePassword = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RegisterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registers");
        }
    }
}
