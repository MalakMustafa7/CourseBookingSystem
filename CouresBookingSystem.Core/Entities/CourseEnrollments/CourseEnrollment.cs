using CouresBookingSystem.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Entities.CourseEnrollments
{
    public class CourseEnrollment : BaseEntity
    {
        
        public string StudentEmail { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
