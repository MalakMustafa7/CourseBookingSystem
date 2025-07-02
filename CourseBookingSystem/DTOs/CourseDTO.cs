using System.ComponentModel.DataAnnotations;

namespace CourseBookingSystem.Api.DTOs
{
    public class CourseDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

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

        [Required]
        public int InstructorId { get; set; }
    }
}
