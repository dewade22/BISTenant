﻿using Manufacturing.Data.Entities;
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
        /*DB Model*/
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
        public virtual DbSet<CountryRegion> CountryRegion { get; set; }
        public virtual DbSet<ShopCalendarHoliday> ShopCalendarHoliday { get; set; }
        public virtual DbSet<SalesShipmentLine> SalesShipmentLine { get; set; }
        public virtual DbSet<SalesShipmentHeader> SalesShipmentHeader { get; set; }
        public virtual DbSet<SalesInvoiceHeader> SalesInvoiceHeader { get; set; }
        public virtual DbSet<SalesCrMemoHeader> SalesCrMemoHeader { get; set; }
        public virtual DbSet<SalesCrMemoLine> SalesCrMemoLine { get; set; }
        public virtual DbSet<ProductionBomheader> ProductionBomheader { get; set; }
        public virtual DbSet<ProductionBomline> ProductionBomline { get; set; }
        public virtual DbSet<ModelMaster> ModelMaster { get; set; }
        public virtual DbSet<ModelDetailMaterial>ModelDetailMaterial { get; set; }
        public virtual DbSet<ModelMachineType>ModelMachineType { get; set; }
        public virtual DbSet<ModelMachineMaster>ModelMachineMaster { get; set; }
        public virtual DbSet<ModelDetailFOHBreakdown> ModelDetailFOHBreakdown { get; set; }
        public virtual DbSet<ModelSubProcess>ModelSubProcess { get; set; }
        public virtual DbSet<ModelRateType>ModelRateType { get; set; }
        public virtual DbSet<ModelRateMaster>ModelRateMaster { get; set; }
        public virtual DbSet<UnitOfMeasures> UnitOfMeasures { get; set; }
        public virtual DbSet<ModelDetail> ModelDetail { get; set; }

        //HPPMixing Model
        public virtual DbSet<ModelWIPProcessHeader> ModelWIPProcessHeader { get; set; }
        public virtual DbSet<ModelWIPProcessLine> ModelWIPProcessLine { get; set; }
        public virtual DbSet<ModelWIPOutput> ModelWIPOutput { get; set; }
        public virtual DbSet<ModelDetailProcess> ModelDetailProcess { get; set; }
        public virtual DbSet<ModelDetailProcessHeader> ModelDetailProcessHeader { get; set; }


        /*Store Procedure Model*/
        public virtual DbSet<spRptSalesActualModel> SpRptSalesActualModels { get; set; }
        public virtual DbSet<spRptSalesActual_LandedCostModel> SpRptSalesActual_LandedCosts { get; set; }
        public virtual DbSet<spSalesInvoiceSummaryPivotModel> SpSalesInvoiceSummaryPivotModels { get; set; }
        public virtual DbSet<spRptInventoryValuationModel> SpRptInventoryValuationModel { get; set; }
        
        public virtual DbSet<spRptSalesBoardModel> SpRptSalesBoardModel { get; set; }

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
            modelBuilder.Entity<spRptSalesActual_LandedCostModel>()
                .HasNoKey();
            modelBuilder.Entity<spSalesInvoiceSummaryPivotModel>()
                .HasNoKey();
            modelBuilder.Entity<spRptInventoryValuationModel>()
                .HasNoKey();
            modelBuilder.Entity<spRptSalesBoardModel>()
               .HasNoKey();
            base.OnModelCreating(modelBuilder);           
        }

        
       
    }
}
