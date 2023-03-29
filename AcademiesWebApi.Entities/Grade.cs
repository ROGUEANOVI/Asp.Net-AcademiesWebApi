using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class Grade
    {
        public int GradeID { get; set; }
        public double Qualification { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }

        public virtual Student Student { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
        
    }
}
