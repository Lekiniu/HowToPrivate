using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HowToWebApplication.Models
{
    public class CategoriesCustomClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

       
        [Display(Name = "ParentCategory")]
        public Nullable<int> ParentId { get; set; }
    }
}