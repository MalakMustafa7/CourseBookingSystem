namespace CourseBookingSystem.Api.DTOs
{
    public class EnrollmentDTO
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public decimal CoursePrice { get; set; }
        public string Status { get; set; }  
        public DateTime EnrolledAt { get; set; }
    }
}
