using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity.Seeding
{
    public static class AppIdenetityDbContextSeed
    {
        public static async Task IdenetitySeed(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var UserSeed = new AppUser()
                {
DisplayName="Nader Rahoumah",
Email="Nader123@gmail.com",
PhoneNumber="01033927215",
UserName="Nader123"

                };
                await userManager.CreateAsync(UserSeed, "Pa$$W0rd");

            }
           

        }
    }
}
