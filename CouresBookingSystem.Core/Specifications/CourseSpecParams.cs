using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Specifications
{
    public class CourseSpecParams
    {
        public int? InstructorId { get; set; }
        private string? category;
        public string? Category
        {
            get { return category; }
            set { category = value.ToLower(); }
        }

        private string? title;
        public string? Title
        {
            get { return title; }
            set { title = value.ToLower(); }
        }

        private string? instructorname;
        public string? InstructorName
        {
            get { return instructorname; }
            set { instructorname = value.ToLower(); }
        }
    }
}
