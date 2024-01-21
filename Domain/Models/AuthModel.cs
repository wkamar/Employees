using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}