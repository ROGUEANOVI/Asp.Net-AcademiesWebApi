using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.CourseDTOs
{
    public class AddCourseRequestDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public int TeacherID { get; set; }
    }
}
