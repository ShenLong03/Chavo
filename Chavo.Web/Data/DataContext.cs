namespace Chavo.Web.Data
{
    using Data.Entity;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Functionary> Functionaries { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Weight> Weights { get; set; }

        public DbSet<UnitLength> UnitLengths { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerProduct> CustomerProducts { get; set; }

        public DbSet<GeneralConfiguration> GeneralConfigurations { get; set; }
    }
}