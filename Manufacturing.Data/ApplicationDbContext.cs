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

namespace Manufacturing.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Item> Items { get; set; }

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

        ///// <summary>
        ///// https://stackoverflow.com/questions/48117961/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-th
        ///// </summary>
        ///// <typeparam name="TEntity"></typeparam>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public override EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException(nameof(entity));
        //    }

        //    var type = entity.GetType();
        //    var et = this.Model.FindEntityType(type);
        //    var key = et.FindPrimaryKey();

        //    var keys = new object[key.Properties.Count];
        //    var x = 0;
        //    foreach (var keyName in key.Properties)
        //    {
        //        var keyProperty = type.GetProperty(keyName.Name, BindingFlags.Public | BindingFlags.Instance);
        //        keys[x++] = keyProperty.GetValue(entity);
        //    }

        //    var originalEntity = Find(type, keys);
        //    if (Entry(originalEntity).State == EntityState.Modified)
        //    {
        //        return base.Update(entity);
        //    }

        //    Entry(originalEntity).CurrentValues.SetValues(entity);
        //    return Entry((TEntity)originalEntity);
        //}

        /// <summary>
        /// Taken from here: https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
        /// </summary>
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
    }
}
