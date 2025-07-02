using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Entities.CourseEnrollments;
using CouresBookingSystem.Core.Repositories;
using CouresBookingSystem.Core.Services;
using CouresBookingSystem.Core.Specifications;
using CouresBookingSystem.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookingContext _context;

        public EnrollmentService(IUnitOfWork unitOfWork,BookingContext Context)
        {
            _unitOfWork = unitOfWork;
            _context = Context;
        }
        public async Task<CourseEnrollment?> CreateEnrollment(string studentEmail, int courseId)
        {
            var spec = new CourseWithInstructorSpecification(courseId);
            var course = await _unitOfWork.Repository<Course>().GetByIdGetEntityWithSpecAsync(spec);
            if(course is  null)   return null;
            var exists = await _context.CourseEnrollments
                    .AnyAsync(e => e.StudentEmail == studentEmail && e.CourseId == courseId);

            if (exists)
                return null;

            var courseEnrollment = new CourseEnrollment
                {
                    StudentEmail = studentEmail,
                    CourseId = courseId,
                    Status = EnrollmentStatus.Approved
                };     
              await _unitOfWork.Repository<CourseEnrollment>().AddAsync(courseEnrollment);
               var Result = await _unitOfWork.CompleteAsync();
              if (Result <= 0) return null;
             return courseEnrollment;
     
        }

        public async Task<bool> DeleteEnrollmentAsync(int id, string studentEmail)
        {
            var enrollment = await GetEnrollmentByIdAsync(id);
            if (enrollment is null) return false;

            if (enrollment.StudentEmail != studentEmail) return false;

            _unitOfWork.Repository<CourseEnrollment>().Delete(enrollment);
            var result = await _unitOfWork.CompleteAsync();

            return result > 0;
        }

        public async Task<IReadOnlyList<CourseEnrollment?>> GetAllEnrollmentsAsync()
        {
            var Spec = new EnrollmentSpecification();
             var Enrollments = await _unitOfWork.Repository<CourseEnrollment>().GetAllWithSpecAsync(Spec);
            if(Enrollments is null) return null;
            return Enrollments;
        }

        public async Task<CourseEnrollment?> GetEnrollmentByIdAsync(int id)
        {
            var Spec = new EnrollmentSpecification(id);
            var Enrollment = await _unitOfWork.Repository<CourseEnrollment>().GetByIdGetEntityWithSpecAsync(Spec);
            if (Enrollment is null) return null;
            return Enrollment;
        }

        public Task<IReadOnlyList<CourseEnrollment?>> GetEnrollmentForCurrentUser(string studentEmail)
        {
             var Spec = new EnrollmentSpecification(studentEmail);
            var Enrollments = _unitOfWork.Repository<CourseEnrollment>().GetAllWithSpecAsync(Spec);
            if(Enrollments is null) return null;
            return Enrollments;
        }
    }
}
