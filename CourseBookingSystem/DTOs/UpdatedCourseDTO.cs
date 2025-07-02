using System.ComponentModel.DataAnnotations;

namespace CourseBookingSystem.Api.DTOs
{
    public class UpdatedCourseDTO
    {
        [Required]
        public int Id { get; set; }   
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
       
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
    }
}
