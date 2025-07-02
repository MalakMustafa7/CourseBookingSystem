using AutoMapper;
using CouresBookingSystem.Core.Entities.CourseEnrollments;
using CouresBookingSystem.Core.Services;
using CourseBookingSystem.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseBookingSystem.Api.Controllers
{ 
    public class EnrollmentController : ApiBaseController
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentService enrollmentService , IMapper mapper)
        {
            _enrollmentService = enrollmentService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost("{courseId}")]
        public async Task<ActionResult<EnrollmentDTO>> BookCourse(int courseId)
        {
            var studentEmail = User.FindFirstValue(ClaimTypes.Email);
            if(studentEmail is null ) 
                   return Unauthorized();
            var courseEnrollment = await _enrollmentService.CreateEnrollment(studentEmail, courseId);
            if (courseEnrollment is null) return BadRequest("You are already enrolled in this course or something went wrong.");
            var EnrollmentDTO = _mapper.Map<CourseEnrollment,EnrollmentDTO>(courseEnrollment);
            return Ok(EnrollmentDTO);
        }
        [Authorize]
        [HttpGet("GetCurrentUserEnrollments")]
        public async Task<ActionResult<IReadOnlyList<EnrollmentDTO>>> GetCurrentUserEnrollments()
        {
            var studentEmail = User.FindFirstValue(ClaimTypes.Email);
            var studentEnrollments = await _enrollmentService.GetEnrollmentForCurrentUser(studentEmail);
            if(studentEnrollments is null) return BadRequest("You have not Enrolled At Any Course Yet");
            var EnrollmentDTO = _mapper.Map< IReadOnlyList<CourseEnrollment>, IReadOnlyList< EnrollmentDTO >>(studentEnrollments);
            return Ok(EnrollmentDTO);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EnrollmentDTO>>> GetAllEnrollments()
        {
            var Enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            if(Enrollments is null) return NotFound();
            var EnrollmentDTO = _mapper.Map<IReadOnlyList<CourseEnrollment>, IReadOnlyList<EnrollmentDTO>>(Enrollments);
            return Ok(EnrollmentDTO);

        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<EnrollmentDTO>> GetEnrollmentById(int Id)
        {
            var Enrollment = await _enrollmentService.GetEnrollmentByIdAsync(Id);
            if(Enrollment is null) return NotFound();
            var studentEmail = User.FindFirstValue(ClaimTypes.Email);
            if (Enrollment.StudentEmail != studentEmail)
                return Unauthorized();
            var EnrollmentDTO = _mapper.Map<CourseEnrollment, EnrollmentDTO>(Enrollment);
            return Ok(EnrollmentDTO);
        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> DeleteEnrollment(int Id)
        {
            var studentEmail = User.FindFirstValue(ClaimTypes.Email);
            if (studentEmail is null) return Unauthorized();

            var result = await _enrollmentService.DeleteEnrollmentAsync(Id, studentEmail);

            if (!result)
                return NotFound("Enrollment not found or you are not authorized to delete it.");

            return Ok("Enrollment deleted successfully.");
        }

    }
}
