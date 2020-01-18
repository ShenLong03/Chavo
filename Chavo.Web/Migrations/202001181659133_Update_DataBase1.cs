namespace Chavo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_DataBase1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PicturesProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FirstLogins", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Revenues", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Revenues", "FunctionaryId", "dbo.Functionaries");
            CreateTable(
                "dbo.CustomerInvestors",
                c => new
                    {
                        CustomerInvestorId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        InvestorId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerInvestorId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Customers", t => t.InvestorId)
                .Index(t => t.CustomerId)
                .Index(t => t.InvestorId);
            
            CreateTable(
                "dbo.InvestorProducts",
                c => new
                    {
                        InvestorProductId = c.Int(nullable: false, identity: true),
                        CustomerInvestorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PercentageProfit = c.Double(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InvestorProductId)
                .ForeignKey("dbo.CustomerInvestors", t => t.CustomerInvestorId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.CustomerInvestorId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Products", "DisplayAcquiereProduct", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Unique", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "RealState", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerProducts", "StatePurchase", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "DisplayShortRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayMediumRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayLongRevenue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayClothes", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "DisplayInversor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "MaxToWin", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "Sizes", c => c.String());
            AddColumn("dbo.Customers", "Comentaries", c => c.String());
            AddColumn("dbo.Customers", "DisplayMoreRealEstate", c => c.Boolean(nullable: false));
            AddColumn("dbo.GeneralConfigurations", "Picture", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "AcquireUserLearningZone", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "VideoCashing", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "SelectClothes", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "TransacctionNumber", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "DescriptionTransacctionNumber", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "MessageLogin", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "MessageFooter", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "MessageContactPage", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "EmailRegisterApproved", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "EmailConfirmed", c => c.String());
            AddColumn("dbo.GeneralConfigurations", "ChangePassword", c => c.String());
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "SubCategoryId");
            AddForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies", "CurrencyId");
            AddForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.PicturesProducts", "ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.FirstLogins", "CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Revenues", "CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Revenues", "FunctionaryId", "dbo.Functionaries", "FunctionaryId");
            DropColumn("dbo.Customers", "Credit");
            DropColumn("dbo.Customers", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Points", c => c.Int());
            AddColumn("dbo.Customers", "Credit", c => c.Double());
            DropForeignKey("dbo.Revenues", "FunctionaryId", "dbo.Functionaries");
            DropForeignKey("dbo.Revenues", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.FirstLogins", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.PicturesProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.InvestorProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.InvestorProducts", "CustomerInvestorId", "dbo.CustomerInvestors");
            DropForeignKey("dbo.CustomerInvestors", "InvestorId", "dbo.Customers");
            DropForeignKey("dbo.CustomerInvestors", "CustomerId", "dbo.Customers");
            DropIndex("dbo.InvestorProducts", new[] { "ProductId" });
            DropIndex("dbo.InvestorProducts", new[] { "CustomerInvestorId" });
            DropIndex("dbo.CustomerInvestors", new[] { "InvestorId" });
            DropIndex("dbo.CustomerInvestors", new[] { "CustomerId" });
            DropColumn("dbo.GeneralConfigurations", "ChangePassword");
            DropColumn("dbo.GeneralConfigurations", "EmailConfirmed");
            DropColumn("dbo.GeneralConfigurations", "EmailRegisterApproved");
            DropColumn("dbo.GeneralConfigurations", "MessageContactPage");
            DropColumn("dbo.GeneralConfigurations", "MessageFooter");
            DropColumn("dbo.GeneralConfigurations", "MessageLogin");
            DropColumn("dbo.GeneralConfigurations", "DescriptionTransacctionNumber");
            DropColumn("dbo.GeneralConfigurations", "TransacctionNumber");
            DropColumn("dbo.GeneralConfigurations", "SelectClothes");
            DropColumn("dbo.GeneralConfigurations", "VideoCashing");
            DropColumn("dbo.GeneralConfigurations", "AcquireUserLearningZone");
            DropColumn("dbo.GeneralConfigurations", "Picture");
            DropColumn("dbo.Customers", "DisplayMoreRealEstate");
            DropColumn("dbo.Customers", "Comentaries");
            DropColumn("dbo.Customers", "Sizes");
            DropColumn("dbo.Customers", "MaxToWin");
            DropColumn("dbo.Customers", "DisplayInversor");
            DropColumn("dbo.Customers", "DisplayClothes");
            DropColumn("dbo.Customers", "DisplayLongRevenue");
            DropColumn("dbo.Customers", "DisplayMediumRevenue");
            DropColumn("dbo.Customers", "DisplayShortRevenue");
            DropColumn("dbo.CustomerProducts", "StatePurchase");
            DropColumn("dbo.Products", "RealState");
            DropColumn("dbo.Products", "Unique");
            DropColumn("dbo.Products", "DisplayAcquiereProduct");
            DropTable("dbo.InvestorProducts");
            DropTable("dbo.CustomerInvestors");
            AddForeignKey("dbo.Revenues", "FunctionaryId", "dbo.Functionaries", "FunctionaryId", cascadeDelete: true);
            AddForeignKey("dbo.Revenues", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.FirstLogins", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.CustomerProducts", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.PicturesProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.CustomerProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CurrencyId", "dbo.Currencies", "CurrencyId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "SubCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
