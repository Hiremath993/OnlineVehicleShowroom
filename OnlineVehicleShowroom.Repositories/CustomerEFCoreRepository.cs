using System;
using System.Collections.Generic;
using System.Text;
using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Customer to inherit EFCoreRepository
    public class CustomerEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public CustomerEFCoreRepository(OVSDbContext context) : base(context)
        {

        }

    }
}
