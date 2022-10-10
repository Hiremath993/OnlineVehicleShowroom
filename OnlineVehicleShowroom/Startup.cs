using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Proxies.Auth;
using OnlineVehicleShowroom.Proxies.Business;
using OnlineVehicleShowroom.Proxies.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVehicleShowroom
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
            services.AddDbContext<OVSDbContext>(options =>
            {
                //options.UseInMemoryDatabase("OnlineVehicleShowroom");

                options.UseSqlServer(Configuration.GetConnectionString("cs"));
            });
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            services.AddScoped<RolesProxy>();
            services.AddScoped<AuthProxy>();
            services.AddScoped<CustomersProxy>();
            services.AddScoped<DealersProxy>();
            services.AddScoped<SalesProxy>();
            services.AddScoped<ShowroomsProxy>();
            services.AddScoped<VehiclesProxy>();
            services.AddScoped<GenerateBillProxy>();
            services.AddScoped<GeneratePurchaseDataProxy>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
