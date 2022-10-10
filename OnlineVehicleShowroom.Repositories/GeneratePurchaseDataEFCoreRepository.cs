using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Generate Purchase Data to inherit EFCoreRepository
    public class GeneratePurchaseDataEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public GeneratePurchaseDataEFCoreRepository(OVSDbContext context) : base(context)
        {

        }
    }
}
