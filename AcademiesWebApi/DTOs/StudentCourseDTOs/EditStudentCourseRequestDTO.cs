using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.StudentCourseDTOs
{
    public class EditStudentCourseRequestDTO
    {
        [Required]
        public int StudentCourseID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }
    }
}
