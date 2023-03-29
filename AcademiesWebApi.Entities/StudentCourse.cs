using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class StudentCourse
    {
        public int StudentCourseID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
