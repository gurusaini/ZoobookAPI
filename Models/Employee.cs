using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoobookAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "First name is out of max length criteria (50)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Middle Name")]
        public string  MiddleName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Last name is out of max lenth criteria (50)")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

    }
}