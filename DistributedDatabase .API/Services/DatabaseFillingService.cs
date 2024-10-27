
using DistributedDatabase.DAL;
using DistributedDatabase.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributedDatabase_.API.Services
{
    public class DatabaseFillingService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseFillingService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DistributedDatabaseDbContext>();

            dbContext.Database.Migrate();

            if(!dbContext.Students.Any() && !dbContext.Courses.Any())  //if db is empty 
            {
                await Fill(dbContext); 
            }
        }

        protected async Task Fill(DistributedDatabaseDbContext dbContext)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Oleksandr", LastName = "Berdnyk" },
                new Student { FirstName = "Emilio", LastName = "Bryant" },
                new Student { FirstName = "Norman", LastName = "Walker" },
                new Student { FirstName = "Diana", LastName = "Smith" },
                new Student { FirstName = "Oleksandr", LastName = "Mitchell" }
            };

            var courses = new List<Course>
            {
                new Course { Title = "Intelligent data analysis", Credits = 4 },
                new Course { Title = "Network information technologies", Credits = 5 },
                new Course { Title = "Project management software tools", Credits = 3 }
            };

            await dbContext.Students.AddRangeAsync(students);
            await dbContext.Courses.AddRangeAsync(courses);
            await dbContext.SaveChangesAsync();

            var studentsCourses = new List<StudentCourse>
            {
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[0].StudentId, CourseId = courses[2].CourseId },
                new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[1].StudentId, CourseId = courses[2].CourseId },
                new StudentCourse { StudentId = students[2].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[2].StudentId, CourseId = courses[1].CourseId },
                new StudentCourse { StudentId = students[2].StudentId, CourseId = courses[2].CourseId },
                new StudentCourse { StudentId = students[3].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[4].StudentId, CourseId = courses[0].CourseId },
                new StudentCourse { StudentId = students[4].StudentId, CourseId = courses[2].CourseId },
            };

            await dbContext.StudentCourses.AddRangeAsync(studentsCourses);
            await dbContext.SaveChangesAsync();
        }
    }
}
