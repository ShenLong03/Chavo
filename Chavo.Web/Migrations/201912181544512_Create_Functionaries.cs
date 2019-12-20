namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Functionaries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Functionaries",
                c => new
                    {
                        FunctionaryId = c.Int(nullable: false, identity: true),
                        ID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
                        Picture = c.String(),
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
                .PrimaryKey(t => t.FunctionaryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Functionaries");
        }
    }
}
