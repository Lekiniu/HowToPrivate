using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HowToWebApplication.Models
{
    public class UsersCustomeEditClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
      
        [Required]
        [Display(Name = "Category")]
        public int CategoriesId { get; set; }

        [Required]
        [Display(Name = "Is User Active?")]
        public bool IsActive { get; set; }
    }
}