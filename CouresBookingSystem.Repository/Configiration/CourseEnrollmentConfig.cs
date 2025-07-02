using CouresBookingSystem.Core.Entities.CourseEnrollments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository.Configiration
{
    public class CourseEnrollmentConfig : IEntityTypeConfiguration<CourseEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourseEnrollment> builder)
        {
            

            builder.HasOne(E=>E.Course)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(E => E.Status)
                   .HasConversion(Estatus=>Estatus.ToString(),Estatus=>(EnrollmentStatus) Enum.Parse(typeof(EnrollmentStatus),Estatus));
                   
        }
    }
}
