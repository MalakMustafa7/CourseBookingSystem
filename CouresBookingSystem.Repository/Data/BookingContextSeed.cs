using CouresBookingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouresBookingSystem.Repository.Data
{
    public class BookingContextSeed
    {
        public static async Task SeedAsync(BookingContext dbContext)
        {
            if (!dbContext.Instructors.Any())
            {
                var instructor = new Instructor
                {
                    FullName = "Dr. Sara Youssef",
                    Email = "sara@example.com",
                    PhoneNumber = "0123456789",
                    Bio = "Expert in AI and Machine Learning",
                    Specialization = "Artificial Intelligence"
                };

                await dbContext.Instructors.AddAsync(instructor);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Courses.Any())
            {
                var course = new Course
                {
                    Title = "Intro to AI",
                    Description = "Basic concepts in Artificial Intelligence",
                    Category = "Technology",
                    Price = 1500,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    Capacity = 30,
                    InstructorId = 1
                };

                await dbContext.Courses.AddAsync(course);
                await dbContext.SaveChangesAsync();
            }


        }
    }
    }

