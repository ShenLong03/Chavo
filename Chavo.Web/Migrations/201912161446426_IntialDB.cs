namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        ID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
                        Picture = c.String(),
                        Credit = c.Double(),
                        Email = c.String(nullable: false),
                        UserName = c.String(),
                        Points = c.Int(),
                        Active = c.Boolean(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                        CellPhone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Genero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
