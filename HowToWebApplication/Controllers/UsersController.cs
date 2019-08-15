
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HowToWebApplication.Models;
using HowToWebApplication.Helpers;
using static HowToWebApplication.Helpers.PasswordHelper;
using HowToWebApplication.Filters;


namespace HowToWebApplication.Controllers
{
    //[AdminFilter]
    public class UsersController : Controller
    {
        HowToDbEntities _db = new HowToDbEntities();
        UsersDataProvider UserData = new UsersDataProvider();
        CategoriesDataProvider CategoriesData = new CategoriesDataProvider();
        ArticlesDataProvider ArticlesData = new ArticlesDataProvider();
        RequestsDataProvider RequestData = new RequestsDataProvider();



        #region User  
        // GET: sers/Details/5
        public ActionResult UserDetails(int id)
        {
            var result = UserData.GetUserById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }
        #endregion

        #region Articles
        // GET: Articles

        public ActionResult ArticlesList(int id )
        {
            ViewBag.Categories = _db.categories.ToList();
            var result = ArticlesData.GetArticlesByUserId(id);
            return View(result);
        }


        // GET: Articles/Details/5
        public ActionResult ArticlesDetails(int id)
        {

            var articleResult = _db.requestsArticles.Where(e => e.articlesId == id).ToList();
            if (articleResult.Count() != 0)
            {
                var requests = new List<requests>();
                foreach (var req in _db.requests)
                {
                    foreach (var artReq in articleResult)
                    {
                        if (req.Id == artReq.requestsId)
                        {
                            requests.Add(req);
                        }
                    }
                }

                if (requests != null)
                {
                    ViewBag.RequestIDs = requests;
                }
            }
            ViewBag.Categories = _db.categories.ToList();
            var result = ArticlesData.GetArticlesById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Articles/Create
        public ActionResult CreateArticles()
        {
            ViewBag.Categories = _db.categories.ToList();
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
            ViewBag.Requests = _db.requests.ToList();
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticles(ArticlesCustomClass model, HttpPostedFileBase[] images)
        {
            ViewBag.Categories = _db.categories.ToList();
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
            //ViewBag.RequestId = new SelectList(_db.requests.ToList(), "Id", "title");
            ViewBag.Requests = _db.requests.ToList();
            if (ModelState.IsValid)
            {
                ArticlesData.CreateArticles(model, images);
                return RedirectToAction("ArticlesList",  "Users", new { id = LoginHelper.CurrentUser().Id });
            }
            else
            {
                return View(model);
            }
        }

        //// GET: Articles/Edit/5
        public ActionResult EditArticles(int id)
        {
            ViewBag.Images = _db.images.Where(e => e.articlesId == id);
            ViewBag.ImagesCount = _db.images.Where(e => e.articlesId == id).Count();
            var result = ArticlesData.GetArticlesById(id);
            ViewBag.Categories = _db.categories.ToList();
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email", result.usersId);
            ViewBag.Requests = _db.requests.ToList();

            //ამ ცვლადში ვპოულობთ უკვე დამატებულ კატეგორიებს და ვაკონვერტირებთ მასივში
            var SelectedCategories = ArticlesData.GetSelectedCatagories(id).Select(e => e.categoriesId).ToArray();
            TempData["PrevSelectedCategories"] = SelectedCategories;// Post-ის edit-თვის გადასაცემი კატეგორიების 
            //ამ ცვლადში ვპოულობთ უკვე დამატებულ მოთხოვნებს და ვაკონვერტირებთ მასივში          
            List<int> Selectedrequests = ArticlesData.GetSelectedRequest(id).Select(e => (int)e.requestsId).ToList();
            TempData["PrevSelectedRequests"] = Selectedrequests;// Post-ის edit-თვის გადასაცემი მოთხოვნების სია

            var customArticle = new ArticlesCustomClass()
            {
                Id = result.Id,
                Title = result.title,
                Content = result.content,
                CategoriesList = SelectedCategories,
                RequestsList = Selectedrequests
            };
            return View(customArticle);
        }


        //// POST: Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticles(ArticlesCustomClass model, HttpPostedFileBase[] images)
        {
            var result = _db.articles.FirstOrDefault(e => e.Id == model.Id);
            ViewBag.Categories = _db.categories.ToList();

            // იმ კატეგორიების წასაშლელი კოდი, რომელიც მომხმარებელმა მულტისელექტლისტიდან ამოშალა
            if (model.CategoriesList != null)
            {
                var PrevSelectedCategories = TempData["PrevSelectedCategories"] as IEnumerable<int>;
                ArticlesData.DeleteUnselectedCategories(model, PrevSelectedCategories);

            }
            // იმ მოთხოვნების წასაშლელი კოდი, რომელიც მომხმარებელმა მულტისელექტლისტიდან ამოშალა          
            var PrevSelectedRequests = TempData["PrevSelectedRequests"] as IEnumerable<int>;
            ArticlesData.DeleteUnselectedRequest(model, PrevSelectedRequests);

            if (ModelState.IsValid)
            {
                ArticlesData.EditArticles(model,images);
                return RedirectToAction("ArticlesList", "Users", new { id = LoginHelper.CurrentUser().Id });
            }
            return View(model);
        }


        // GET: Articles/Delete/5
        public ActionResult DeleteArticle(int id)
        {
            ViewBag.Categories = _db.categories.ToList();
            var result = ArticlesData.GetArticlesById(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Article/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticle(articles model)
        {

            try
            {
                ArticlesData.FullDeleteArticle(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("ArticlesList", "Users", new { id = LoginHelper.CurrentUser().Id });
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteImages(int id)
        {
            var articleIds = _db.articles.FirstOrDefault(e => e.Id == id);
            var result = _db.images.FirstOrDefault(e => e.articlesId == articleIds.Id);
            try
            {
                ArticlesData.DeleteImages(result);
            }
            catch
            {
                return View(result);
            }
            return RedirectToAction("ArticlesList", "Users", new { id = LoginHelper.CurrentUser().Id });
        }


   
        #endregion


        #region Requests
        // GET: Requests

        public ActionResult RequestsList(int id)
        {
            ViewBag.Categories = _db.categories.ToList();
            var result = RequestData.GetRequestsByUserId(id);
            return View(result);
        }


        // GET: Articles/Details/5
        public ActionResult RequestsDetails(int id)
        {
            var articleResult = _db.requestsArticles.FirstOrDefault(e => e.requestsId == id);
            var articleIds = _db.articles.Where(e => e.Id == articleResult.articlesId).Count();
            if (articleIds != 0)
            {
                ViewBag.ArticleID = _db.articles.FirstOrDefault(e => e.Id == articleResult.articlesId).Id;
            }
            ViewBag.Categories = _db.categories.ToList();
            var result = RequestData.GetRequestById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Articles/Create
        public ActionResult CreateRequests()
        {
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
            return View();
        }

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRequests(RequestsCustomClass model)
        {
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
            if (ModelState.IsValid)
            {
                RequestData.CreateRequest(model);
                return RedirectToAction("RequestsList", "Users", new { id = LoginHelper.CurrentUser().Id });
            }
            else
            {
                return View(model);
            }
        }

        //// GET: Requests/Edit/5
        public ActionResult EditRequests(int id)
        {

            var result = RequestData.GetRequestById(id);
            ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email", result.usersId);

            var customRequest = new RequestsCustomClass()
            {
                Title = result.title,
                Content = result.content,
                //IsDone = result.isDone,
                //Upvote = result.upvote,
                //UsersId = result.usersId,

            };
            return View(customRequest);
        }


        //// POST: Requests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRequests(RequestsCustomClass model)
        {

            if (ModelState.IsValid)
            {
                RequestData.EditRequest(model);
                return RedirectToAction("RequestsList", "Users", new { id = LoginHelper.CurrentUser().Id });
            }
            return View(model);
        }




        // GET: Requests/Delete/5
        public ActionResult DeleteRequests(int id)
        {
            ViewBag.Categories = _db.categories.ToList();
            var result = RequestData.GetRequestById(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Requests/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRequests(requests model)
        {

            try
            {
                RequestData.FullDeleteRequest(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("RequestsList", "Users", new { id = LoginHelper.CurrentUser().Id });
        }




        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(articles model)
        //{
        //    try
        //    {
        //        ArticlesData.deleteCategories(model);
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //    return RedirectToAction("index");
        //}

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}