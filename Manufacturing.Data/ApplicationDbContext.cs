using Manufacturing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;
using Manufacturing.Domain;
using Manufacturing.Domain.Data;
using System;
using System.Linq;
using Manufacturing.Domain.Multitenancy;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Manufacturing.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<ItemLedgerEntry> ItemLedgerEntry { get; set; }
        public virtual DbSet<ItemBudgetEntry> ItemBudgetEntry { get; set; }
        public virtual DbSet<SalesInvoiceLine> SalesInvoiceLine { get; set; }
        public virtual DbSet<InventoryPostingGroup> InventoryPostingGroup { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<ProductGroup> ProductGroup { get; set; }
        public virtual DbSet<DimensionValue> DimensionValue { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }
        public virtual DbSet<Dashboards_Info> Dashboards_Info { get; set; }
        public virtual DbSet<Element> Element { get; set; }
        public virtual DbSet<DashboardLinkedElements> DashboardLinkedElements { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(IHttpContextAccessor httpContextAccessor, SystemDbContext systemsDbContex)
           : base(CreateDbContextOptions(httpContextAccessor, systemsDbContex))
        {
        }

        private static DbContextOptions CreateDbContextOptions(IHttpContextAccessor httpContextAccessor, SystemDbContext systemsDbContext)
        {
            var companyCode = httpContextAccessor.HttpContext.GetClient().CompanyCode;
            //var companyCode = "AVJewel";
            var connectionString = systemsDbContext.CompanyInformation.SingleOrDefault(Client => Client.CompanyCode == companyCode).ConnectionString;

            if (connectionString == null)
            {
                throw new NullReferenceException($"The connection string was null for the company: companyCode");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            return optionsBuilder.UseSqlServer(connectionString).Options;
        }

        
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Manufacturing/appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                var connectionString = configuration.GetConnectionString("AVJewel");
                builder.UseSqlServer(connectionString);
                return new ApplicationDbContext(builder.Options);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
