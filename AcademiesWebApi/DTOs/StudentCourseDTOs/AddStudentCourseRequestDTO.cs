using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.StudentCourseDTOs
{
    public class AddStudentCourseRequestDTO
    {
        
        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }

    }
}
