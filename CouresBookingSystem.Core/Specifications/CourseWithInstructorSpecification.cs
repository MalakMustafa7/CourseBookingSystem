using CouresBookingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Specifications
{
    public class CourseWithInstructorSpecification : BaseSpecifications<Course>
    {
        public CourseWithInstructorSpecification(CourseSpecParams param) : base(C=>
        (string.IsNullOrEmpty(param.Title) || C.Title.ToLower().Contains(param.Title))
        &&
        (string.IsNullOrEmpty(param.Category) || C.Category.ToLower().Contains(param.Category))
        &&
        (string.IsNullOrEmpty(param.InstructorName) || C.Instructor.FullName.ToLower().Contains(param.InstructorName) )
        &&
        (!param.InstructorId.HasValue || C.InstructorId==param.InstructorId)
        )
        {
            Includes.Add(C => C.Instructor);
        }
        public CourseWithInstructorSpecification(int id):base(C=>C.Id==id)
        {
            Includes.Add(C => C.Instructor);
        }

    }
}
