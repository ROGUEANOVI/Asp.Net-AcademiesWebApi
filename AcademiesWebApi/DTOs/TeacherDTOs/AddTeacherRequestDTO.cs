using AcademiesWebApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.TeacherDTOs
{
    public class AddTeacherRequestDTO
    {

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public int SchoolID { get; set; }
    }
}
