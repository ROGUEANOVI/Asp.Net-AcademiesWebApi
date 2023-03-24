using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Web { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Teacher>? Teachers { get; set; }
    }
}
