using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HowToWebApplication.Models
{
    public class ArticleContext : DbContext
    {
        public DbSet<articles> Articles { get; set; }
    }
}