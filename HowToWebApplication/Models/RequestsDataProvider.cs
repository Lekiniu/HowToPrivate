using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HowToWebApplication.Helpers;

namespace HowToWebApplication.Models
{
    public class RequestsDataProvider
    {
        HowToDbEntities _db = new HowToDbEntities();
        
        

        public bool Exist(requests request)
        {
            return _db.requests.FirstOrDefault(e => e.content == request.content) == null ? false : true;
        }

        public bool ExistCustomRequest(RequestsCustomClass article)
        {
            return _db.requests.FirstOrDefault(e => e.content == article.Content) == null ? false : true;
        }

        public requests GetRequestById(int id)
        {
            return _db.requests.FirstOrDefault(e => e.Id == id);
        }

        


        public void CreateRequest(RequestsCustomClass request)
        {
            //var dateString = DateTime.Now.ToString("yyyyMMdd");
            var newRequest = new requests();
            newRequest.title = request.Title;
            newRequest.number = null;
            newRequest.content = request.Content;
            newRequest.date = DateTime.Now;
           // newRequest.upvote = request.Upvote;
            newRequest.isDone = false;
            if (LoginHelper.IsLoggedIn())
            {
                newRequest.usersId = LoginHelper.CurrentUser().Id;
            }
            else
            {
                newRequest.usersId = 16; // რომ წავა დასასრულისკენ პროექტი, ეს იფ-ელსი წაიშლება და მარტო current user დარჩება 
            }

            if (!ExistCustomRequest(request))
            {
                _db.requests.Add(newRequest);
                _db.SaveChanges();

                //რადგან აიდი მხოლოდ ბაზაში ჩაწერის შემდეგ ენიჭება, ნუმერაციას ბაზაში ჩაწერის შემდეგ ვანიჭებთ, მანამდე ნუმერაცია ნალია
                var dateString = DateTime.Now.ToString("yyyyMM"); // თარიღი სტრინგად
                newRequest.number = Int32.Parse(dateString + newRequest.Id); // თარიღი+ახალი აიდი
                _db.SaveChanges();


                _db.requestsArticles.Add(
                   new requestsArticles()
                   {
                       requestsId = newRequest.Id,
                       articlesId = null
                   });
                _db.SaveChanges();
            }
           
        }
  
        //Edit 
        public void EditRequest(RequestsCustomClass request)
        {
            var result = _db.requests.FirstOrDefault(e => e.Id == request.Id);

            if (!ExistCustomRequest(request) || result.content == request.Content)
            {
                result.title = result.title;
                result.content = request.Content;
                //result.upvote = 0;
                //result.isDone = request.IsDone;
                //result.usersId = request.UsersId;
            }
            _db.SaveChanges();
        }


        public void FullDeleteRequest(requests request)

        {          
            var deleteRequestsArticles = _db.requestsArticles.Where(e => e.requestsId == request.Id);
            var deleteRequests = _db.requests.Where(e => e.Id == request.Id);

            _db.requestsArticles.RemoveRange(deleteRequestsArticles);
            _db.requests.RemoveRange(deleteRequests);
            _db.SaveChanges();
        }


        public IEnumerable<requests> AllRequest()
        {
            return _db.requests;
        }


        public IEnumerable<requests> GetRequestsByUserId(int id)
        {
            var getUser = _db.users.FirstOrDefault(e => e.Id == id);
            return _db.requests.Where(e => e.usersId == getUser.Id);
        }


        public void DoneRequest(int id)
        {
            var request = _db.requests.FirstOrDefault(e => e.Id == id);
            request.isDone = true;
            _db.SaveChanges();
        }
        public void UnDoneRequest(int id)
        {
            var request = _db.requests.FirstOrDefault(e => e.Id == id);
            request.isDone = false;
            _db.SaveChanges();
        }
    }
}
