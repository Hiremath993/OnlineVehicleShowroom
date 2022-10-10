using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Vehicle to inherit EFCoreRepository
    public class VehicleEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public VehicleEFCoreRepository(OVSDbContext context) : base(context)
        {

        }

    }
}
