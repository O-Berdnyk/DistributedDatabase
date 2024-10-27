using DistributedDatabase.DAL.DTOs;
using DistributedDatabase.DAL.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DistributedDatabase_.API.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<StudentDTO> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient
        mongoClient)
        {
            var mongoDatabase =
            mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<StudentDTO>("Students");
        }

        public async Task<List<StudentDTO>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();

        public async Task<StudentDTO> GetByIdAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(StudentDTO student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, StudentDTO updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}
