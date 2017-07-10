using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Person
    {
        [Required]
        [Range(0, 90, ErrorMessage = "Can only be between 0 .. 90")]
        public int id;
        [Required]
        [MaxLength(12)]
        [MinLength(1)]
        public string Name;
        [Required]
        [MaxLength(12)]
        [MinLength(1)]
        public string Gender;
        [Required]
        [Range(0, 90, ErrorMessage = "Can only be between 0 .. 90")]
        public int Age;
    }
}