﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class StudentCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}