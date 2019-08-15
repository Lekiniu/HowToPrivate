using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace HowToWebApplication.Models
{
    public class ArticlesCustomClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }


        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }


        public int UsersId { get; set; }

        public List<categories> Categories { get; set; }

        public List<requests> Requests { get; set; }


        [Required]
        [Display(Name = "Category")]
        public int[] CategoriesList { get; set; }


        [Display(Name = "Article title")]
        public List<int> RequestsList { get; set; }

    }
}