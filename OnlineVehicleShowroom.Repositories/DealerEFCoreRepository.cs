using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Dealer to inherit EFCoreRepository
    public class DealerEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public DealerEFCoreRepository(OVSDbContext context) : base(context)
        {

        }

    }
}
