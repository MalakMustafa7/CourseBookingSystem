using AutoMapper;
using CouresBookingSystem.Core.Repositories;
using CouresBookingSystem.Core.Services;
using CouresBookingSystem.Repository;
using CouresBookingSystem.Service;
using CourseBookingSystem.Api.Helper;

namespace CourseBookingSystem.Api.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IEnrollmentService),typeof(EnrollmentService));
            return services;
        }
    }
}
