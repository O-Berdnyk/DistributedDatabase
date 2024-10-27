using DistributedDatabase.DAL.Enums;

namespace DistributedDatabase.DAL.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public TeacherRank Rank { get; set; }

        public ICollection<TeacherCourse> TeacherCourses { get; set; } = null!;
    }
}
