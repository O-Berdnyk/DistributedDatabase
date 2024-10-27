using DistributedDatabase.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributedDatabase.DAL
{
    public class DistributedDatabaseDbContext : DbContext
    {
        public DistributedDatabaseDbContext(DbContextOptions<DistributedDatabaseDbContext> options)
            : base(options) 
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<TeacherCourse>()
                .HasKey(sc => new { sc.TeacherId, sc.CourseId });

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(sc => sc.Teacher)
                .WithMany(s => s.TeacherCourses)
                .HasForeignKey(sc => sc.TeacherId);

            modelBuilder.Entity<TeacherCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
