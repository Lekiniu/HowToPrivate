using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HowToWebApplication.Models
{
    public class RequestsCustomClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]     
        public int Number { get; set;}

        [Required]
        [Display(Name = "title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "upvote numbers")]
        public int Upvote { get; set; }

        [Required]
        [Display(Name = "Is Done?")]
        public bool IsDone { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UsersId { get; set; }

    }
}