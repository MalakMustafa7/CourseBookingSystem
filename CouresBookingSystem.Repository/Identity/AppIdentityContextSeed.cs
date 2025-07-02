using CouresBookingSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository.Identity
{
    public class AppIdentityContextSeed 
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    FullName = "Aliaa Tarek",
                    Email = "aliaatarek.route@gmail.com",
                    UserName = "aliaatarek.route",
                    PhoneNumber = "01234567891",
                    University="Cairo",
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
