using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineVehicleShowroom.Entities.Auth
{
    //Declaring properties for listing view of Role and its members
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<IdentityUser> Members { get; set; }

        public IEnumerable<IdentityUser> NonMembers { get; set; }
    }
}
