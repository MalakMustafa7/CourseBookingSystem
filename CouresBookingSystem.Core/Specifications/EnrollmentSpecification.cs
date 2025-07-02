using CouresBookingSystem.Core.Entities.CourseEnrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Specifications
{
    public class EnrollmentSpecification : BaseSpecifications<CourseEnrollment>
    {
        public EnrollmentSpecification() : base() {
            Includes.Add(E => E.Course);
        }
        public EnrollmentSpecification(int id) : base(E=>E.Id==id)
        {
            Includes.Add(E => E.Course);
        }
        public EnrollmentSpecification(string Email) : base(S=>S.StudentEmail == Email) {
            Includes.Add(E => E.Course);
        }
    }
}
