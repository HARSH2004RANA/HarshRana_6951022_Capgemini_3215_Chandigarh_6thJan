using System.ComponentModel.DataAnnotations;
namespace UniversityManagmentSystem.Models
{
    

    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime EnrollmentDate { get; set; }

        // Navigation Property
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
