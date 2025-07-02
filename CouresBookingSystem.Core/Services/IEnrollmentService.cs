using CouresBookingSystem.Core.Entities.CourseEnrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Core.Services
{
    public interface IEnrollmentService
    {
        public Task<CourseEnrollment?> CreateEnrollment(string studentEmail, int courseId);
        public Task<IReadOnlyList<CourseEnrollment?>> GetEnrollmentForCurrentUser(string studentEmail);
        public Task<IReadOnlyList<CourseEnrollment?>> GetAllEnrollmentsAsync();
        public Task<CourseEnrollment?> GetEnrollmentByIdAsync(int id);
        public Task<bool> DeleteEnrollmentAsync(int id, string studentEmail); 

    }
}
