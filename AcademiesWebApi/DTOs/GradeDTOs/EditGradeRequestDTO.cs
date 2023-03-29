using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.GradeDTOs
{
    public class EditGradeRequestDTO
    {
        [Required]
        public int GradeID { get; set; }

        [Required]
        public double Qualification { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }
    }
}
