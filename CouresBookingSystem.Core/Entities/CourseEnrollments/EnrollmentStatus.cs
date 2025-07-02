using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Entities.CourseEnrollments
{
    public enum EnrollmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Enrollment Approved")]
        Approved,
        [EnumMember(Value = "Enrollment Rejected")]
        Rejected,
        [EnumMember(Value = "Enrollment Cancelled")]
        Cancelled
    }
}
