using MongoDB.Driver;
using DistributedDatabase.DAL;
using System.Transactions;
using DistributedDatabase.DAL.DTOs;

namespace DistributedDatabase_.API.Services
{
    public class UpdateService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly StudentService _studentService;

        public UpdateService(IServiceScopeFactory serviceScopeFactory, StudentService studentService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _studentService = studentService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using (var scope = _serviceScopeFactory.CreateAsyncScope())
            using (var dbContext = scope.ServiceProvider.GetRequiredService<DistributedDatabaseDbContext>())
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var students = dbContext.Students.ToList();
                    foreach (var student in students)
                    {
                        var studentDto = new StudentDTO()
                        {
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Birthday = student.Birthday,
                            Group = student.Group,
                        };
                        studentDto.Id = studentDto.FirstName + studentDto.LastName + studentDto.Birthday.ToLongDateString();
                        await _studentService.CreateAsync(studentDto);
                    }
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    throw;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
