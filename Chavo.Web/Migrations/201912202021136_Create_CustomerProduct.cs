namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_CustomerProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerProducts",
                c => new
                    {
                        CustomerProductId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerProducts");
        }
    }
}
