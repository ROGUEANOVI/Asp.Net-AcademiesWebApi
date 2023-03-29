using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.SchoolDTOs
{
    public class EditSchoolRequestDTO
    {
        [Required]
        public int SchoolID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Web { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }
    }
}
