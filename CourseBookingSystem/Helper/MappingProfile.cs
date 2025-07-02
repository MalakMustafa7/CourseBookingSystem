using AutoMapper;
using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Entities.CourseEnrollments;
using CourseBookingSystem.Api.DTOs;

namespace CourseBookingSystem.Api.Helper
{
    public class MappingProfile : Profile
    {
      public MappingProfile() {
            CreateMap<CourseDTO,Course>().ReverseMap();
            CreateMap<UpdatedCourseDTO, Course>().ReverseMap();

            CreateMap<CourseEnrollment, EnrollmentDTO>()
                     .ForMember(e => e.CourseTitle, ce => ce.MapFrom(c => c.Course.Title))
                     .ForMember(e => e.CourseDescription, ce => ce.MapFrom(c => c.Course.Description))
                     .ForMember(e => e.CoursePrice, ce => ce.MapFrom(c => c.Course.Price))
                     .ForMember(e => e.Status, ce => ce.MapFrom(c =>c.Status.ToString()));

            CreateMap<EnrollmentDTO, CourseEnrollment>()
                     .ForMember(e => e.Status, ce => ce.MapFrom(c => Enum.Parse<EnrollmentStatus>(c.Status)));  
        }
    }
}
