using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Manufacturing.Data;
using Microsoft.AspNetCore.Http;
using Manufacturing.Services;
using Manufacturing.Domain.Data;
using Manufacturing.Domain.Multitenancy;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace Manufacturing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SystemDbContext>(options => options.UseSqlServer(
                Configuration["Data:ManufacturingMainSystem:ConnectionString"]));

            services.AddScoped<ApplicationDbContext>();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            //    Configuration["Data:ManufacturingApplication:ConnectionString"]));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ReturnUrlParameter = "/";
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        var CompanyCode = context.HttpContext.GetClient().CompanyCode;

                        if (Configuration.GetValue<bool>("UsePathToResolveClient"))
                        {
                            context.Response.Redirect(new PathString($"/{CompanyCode}/Account/Login"));
                        }
                        else
                        {
                            context.Response.Redirect(new PathString("/Account/Index"));
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IItemService, ItemService>();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            if (Configuration.GetValue<bool>("UseRedis"))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = Configuration["RedisConfig:Configuration"];
                    options.InstanceName = Configuration["RedisConfig:InstanceName"];
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            //services.AddScoped<ISettingService, SettingsService>();

            services.AddMultiClient()
              .WithResolutionStrategy<HostResolutionStrategy>()
              .WithStore<CompanyInformationDbStore>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMultiTenancy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Account}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                if (Configuration.GetValue<bool>("UsePathToResolveClient"))
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{Client}/{controller=Home}/{action=Index}/{id?}");
                }
                else
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                }

                endpoints.MapRazorPages();
            });

        }
    }
}
