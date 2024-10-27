namespace DistributedDatabase.DAL.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public int Group { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = null!;

    }
}
