using OnlineVehicleShowroom.DataAccessLayer;
using OnlineVehicleShowroom.Repositories.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Repositories
{
    //Declaring specific class for Showroom to inherit EFCoreRepository
    public class ShowroomEFCoreRepository<T> : EFCoreRepository<T> where T : class
    {
        public ShowroomEFCoreRepository(OVSDbContext context) : base(context)
        {

        }

    }
}
