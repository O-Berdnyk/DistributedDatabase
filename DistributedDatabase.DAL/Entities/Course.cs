namespace DistributedDatabase.DAL.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = null!;
        public int Credits { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = null!;
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = null!;
    }
}
