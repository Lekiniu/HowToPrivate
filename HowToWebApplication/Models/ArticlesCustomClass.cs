﻿using System;
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
        //[Display(Name = "Title")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Global),
        //          ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Global))]
        public string Title { get; set; }


        [Required]
        [AllowHtml]
        //[Display(Name = "Content")]
        //[Required(ErrorMessageResourceType = typeof(Resources.Global),
        //          ErrorMessageResourceName = "CountryRequired")]
        [Display(Name = "Content", ResourceType = typeof(Resources.Global))]
        public string Content { get; set; }

       
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "IsBlocked")]
        public bool IsBlocked { get; set; }

        [Display(Name = "User")]
        public users User { get; set; }

        public int UsersId { get; set; }

        public List<categories> Categories { get; set; }

        public List<requests> Requests { get; set; }

        public HttpPostedFileBase[] Images { get; set; }

        public IEnumerable<images> ImagesList { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int[] CategoriesList { get; set; }


        [Display(Name = "Article title")]
        public List<int> RequestsList { get; set; }

    }
}