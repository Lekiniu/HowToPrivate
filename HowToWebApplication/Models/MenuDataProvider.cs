using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowToWebApplication.Models
{
    public class ApplicationDataProvider
    {
        HowToDbEntities _db = new HowToDbEntities();

      

        public IEnumerable<articles> GetArticles()
        {
            return _db.articles;
        }
        public IEnumerable<requests> GetRequest()
        {
            return _db.requests;
        }

        public IEnumerable<users> GetUsers()
        {
            return _db.users;
        }
       

        public IEnumerable<websiteInfos> GetWebSiteInfos()
        {
            return _db.websiteInfos;
        }
     
      
    }
}