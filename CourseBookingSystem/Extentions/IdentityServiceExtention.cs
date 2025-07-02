using CouresBookingSystem.Core.Entities.Identity;
using CouresBookingSystem.Core.Services;
using CouresBookingSystem.Repository.Identity;
using CouresBookingSystem.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CourseBookingSystem.Api.Extentions
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services , IConfiguration configuration) {
            services.AddScoped<ITokenService,TokenService>();

            services.AddIdentity<AppUser,IdentityRole>()
                   .AddEntityFrameworkStores<AppIdentityContext> ();

            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                     .AddJwtBearer(Options =>
                     {
                         Options.TokenValidationParameters = new TokenValidationParameters()
                         {
                             ValidateIssuer = true,
                             ValidIssuer = configuration["JWT:ValidIssuer"],
                             ValidateAudience = true,
                             ValidAudience = configuration["JWT:ValidAuddiance"],
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                         };
                     });
            return services;
        }
    }
}
