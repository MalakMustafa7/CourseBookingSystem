using CouresBookingSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Services
{
    public interface ITokenService
    {
        public Task<string> GetTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
