using Manufacturing.Domain.Multitenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Manufacturing.Domain.Data
{
    public class SystemDbContext : DbContext
    {
        //public SystemDbContext()
        //{
        //}

        public DbSet<Client> CompanyInformation { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
        public DbSet<SystemObject> SystemObject { get; set; }
        public DbSet<SystemPermission> SystemPermissions { get; set; }
        public DbSet<SystemRoles> SystemRoles { get; set; }
        public DbSet<SystemUserMenu> SystemUserMenu { get; set; }
        public DbSet<SystemUserRoles> SystemUserRoles { get; set; }
        public DbSet<WebLoginActivity> webLoginActivity { get; set; }

        public SystemDbContext(DbContextOptions<SystemDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Taken from here: https://medium.com/oppr/net-core-using-entity-framework-core-in-a-separate-project-e8636f9dc9e5
        /// </summary>
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SystemDbContext>
        {
            public SystemDbContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    //.AddJsonFile(@Directory.GetCurrentDirectory() + "/../Manufacturing/tenantsconfig.json")
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../Manufacturing/appsetting.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<SystemDbContext>();
                var connectionString = configuration.GetConnectionString("Balimoon");
                builder.UseSqlServer(connectionString);
                return new SystemDbContext(builder.Options);
            }
        }

    }
}
