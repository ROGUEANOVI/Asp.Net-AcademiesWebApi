using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public enum IdType {
        TI = 1,
        CC = 2
    }
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public IdType IdentificationType  { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int SchoolID { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual ICollection<StudentCourse>? StudentsCourses { get; set; }
    }
}
