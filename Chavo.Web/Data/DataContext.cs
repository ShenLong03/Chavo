﻿namespace Chavo.Web.Data
{
    using Data.Entity;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
           
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new CustomerInvestorMap());
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

        public DbSet<Register> Registers { get; set; }

        public DbSet<Revenue> Revenues { get; set; }

        public DbSet<PicturesProduct> PicturesProducts { get; set; }

        public DbSet<FirstLogin> FirstLogins { get; set; }

        public DbSet<CustomerInvestor> CustomerInvestors { get; set; }

        public DbSet<InvestorProduct> InvestorProducts { get; set; }
    }
}