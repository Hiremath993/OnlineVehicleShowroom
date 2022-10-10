using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Generate Bill to inherit EFCoreRepository
    public class GenerateBillEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public GenerateBillEFCoreRepository(OVSDbContext context) : base(context)
        {

        }
    }
}
