using AutoMapper;
using CouresBookingSystem.Core.Entities;
using CouresBookingSystem.Core.Repositories;
using CouresBookingSystem.Core.Specifications;
using CourseBookingSystem.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingSystem.Api.Controllers
{
     
    public class CourseController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<CourseDTO>>CreateCourse(CourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var mappedCourse = _mapper.Map<CourseDTO,Course>(courseDTO);
            await _unitOfWork.Repository<Course>().AddAsync(mappedCourse);
            var result = await _unitOfWork.CompleteAsync();
            if(result<=0) return BadRequest("Failed to create the course");
            var Course = _mapper.Map<Course, CourseDTO>(mappedCourse);
            return Ok(Course);
        }
         
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Course>>> GetCourses([FromQuery] CourseSpecParams param)
        {
            var Spec = new CourseWithInstructorSpecification(param);
            var Courses = await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(Spec);
            if (!Courses.Any())
                return NotFound("There are no courses available.");
            return Ok(Courses);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>>GetCourseById(int id)
        {
            var Spec = new CourseWithInstructorSpecification(id);
            var Course = await _unitOfWork.Repository<Course>().GetByIdGetEntityWithSpecAsync(Spec);
            if (Course is null) return NotFound("There is No Course With This Id");
            return Ok(Course);
        }
        [HttpPut]
        public async Task<ActionResult<UpdatedCourseDTO>> UpdateCourse(UpdatedCourseDTO UpdatedCourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCourse = await _unitOfWork.Repository<Course>().GetByIdAsync(UpdatedCourse.Id);
            if (existingCourse == null)
                return NotFound("Course not found.");

            _mapper.Map(UpdatedCourse, existingCourse);

            _unitOfWork.Repository<Course>().Update(existingCourse);
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return BadRequest("Failed to Update the course");

            var CourseDTO = _mapper.Map<Course, UpdatedCourseDTO>(existingCourse);
            return Ok(CourseDTO);
        }

        [HttpDelete("{id}")]
        public  async Task<ActionResult> Delete(int id) {
            var Spec = new CourseWithInstructorSpecification(id);
            var course = await _unitOfWork.Repository<Course>().GetByIdGetEntityWithSpecAsync(Spec);
            if (course is null) return NotFound("Course not found.");

            _unitOfWork.Repository<Course>().Delete(course);
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0) return BadRequest("Failed to delete the course.");

            return Ok("Course deleted successfully.");

        }
    }
}
