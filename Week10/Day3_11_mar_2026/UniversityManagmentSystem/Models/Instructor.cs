namespace UniversityManagmentSystem.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
