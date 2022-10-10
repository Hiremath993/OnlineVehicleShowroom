using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineVehicleShowroom.Entities.Auth;
using OnlineVehicleShowroom.Entities.Business;
using OnlineVehicleShowroom.Entities.Invoice;
using System;

namespace OnlineVehicleShowroom.DataAccessLayer
{
    public class OVSDbContext : IdentityDbContext<ApplicationUser>
    {
        //Creating DbSet properties for each entity for CRUD Operations
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Showroom> Showrooms { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Sales> Sales { get; set; }      
        public DbSet<GenerateBill> GenerateBill { get; set; }
        public DbSet<GeneratePurchaseData> GeneratePurchaseData { get; set; }

        //Declaring zero-parameterized constructor
        public OVSDbContext() : base()
        {

        }

        //Declaring parameterized constructor with DbContextOptions parameter
        public OVSDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
