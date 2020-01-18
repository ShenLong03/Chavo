
namespace Chavo.Common
{
    using System.Data.Entity.ModelConfiguration;

    internal class CustomerInvestorMap : EntityTypeConfiguration<CustomerInvestor>
    {
        public CustomerInvestorMap()
        {
            HasRequired(o => o.Customer)
                .WithMany(m => m.Investors)
                .HasForeignKey(m => m.CustomerId);

            HasRequired(o => o.Investor)
                .WithMany(m => m.Customers)
                .HasForeignKey(m => m.InvestorId);
        }
    }
}