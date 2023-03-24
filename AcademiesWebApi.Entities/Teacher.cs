﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int SchoolId { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Course>? Courses { get; set; }
    }
}