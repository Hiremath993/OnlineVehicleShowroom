using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Entities.Auth;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Entities.Invoice;
using OnlineVehicleShowroom.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories.Extensions
{
    public static class EFCoreRepositoryExtensions
    {
        //Declaring static method AddRepositories to add repositories to the services collection as a dependency
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            //Adding DbContext to specify the database to be used
            services.AddDbContext<OVSDbContext>(options =>
            {
                //options.UseInMemoryDatabase("OnlineVehicleShowroom");

                options.UseSqlServer(configuration.GetConnectionString("cs"), b => b.MigrationsAssembly("OnlineVehicleShowroom.WebAPIs"));
            });

            //Adding Identity for Security purpose
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OVSDbContext>()
                .AddDefaultTokenProviders();

            //Adding Scoped service for all entities
            services.AddScoped<IRepository<Customer>, CustomerEFCoreRepository<Customer>>();
            services.AddScoped<IRepository<Dealer>, DealerEFCoreRepository<Dealer>>();
            services.AddScoped<IRepository<Sales>, SalesEFCoreRepository<Sales>>();
            services.AddScoped<IRepository<Showroom>, ShowroomEFCoreRepository<Showroom>>();
            services.AddScoped<IRepository<Vehicle>, VehicleEFCoreRepository<Vehicle>>();
            services.AddScoped<IRepository<GenerateBill>, GenerateBillEFCoreRepository<GenerateBill>>();
            services.AddScoped<IRepository<GeneratePurchaseData>, GeneratePurchaseDataEFCoreRepository<GeneratePurchaseData>>();

            return services;
        }
    }
}
