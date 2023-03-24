﻿using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs
{
    public class RegisterUserRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
