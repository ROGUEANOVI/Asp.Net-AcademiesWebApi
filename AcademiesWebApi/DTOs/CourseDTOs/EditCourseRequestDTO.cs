using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.CourseDTOs
{
    public class EditCourseRequestDTO
    {
        [Required]
        public int CourseID { get; set; }
       
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int TeacherID { get; set; }
    }
}
