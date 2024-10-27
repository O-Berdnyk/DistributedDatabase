namespace DistributedDatabase.DAL.DTOs
{
    public class StudentDTO
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Birthday { get; set; }
        public int Group { get; set; }
    }
}
