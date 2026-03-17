using System.ComponentModel.DataAnnotations;
namespace UniversityManagmentSystem.Models
{


    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}