using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedDatabase.DAL.Entities
{
    public class TeacherCourse
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
