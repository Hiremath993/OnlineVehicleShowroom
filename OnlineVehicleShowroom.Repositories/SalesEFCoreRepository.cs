using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Sales to inherit EFCoreRepository
    public class SalesEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public SalesEFCoreRepository(OVSDbContext context) : base(context)
        {

        }

    }
}
