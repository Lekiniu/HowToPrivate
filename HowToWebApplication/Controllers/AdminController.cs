using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HowToWebApplication.Models;
using HowToWebApplication.Helpers;
using static HowToWebApplication.Helpers.PasswordHelper;
using HowToWebApplication.Filters;
using System.Threading.Tasks;

namespace HowToWebApplication.Controllers
{
    //[AdminFilter]
    public class AdminController : Controller
    {
        HowToDbEntities _db = new HowToDbEntities();
        UsersDataProvider UserData = new UsersDataProvider();
        CategoriesDataProvider CategoriesData = new CategoriesDataProvider();
        ArticlesDataProvider ArticlesData = new ArticlesDataProvider();
        RequestsDataProvider RequestData = new RequestsDataProvider();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Articles
        // GET: Articles

        public ActionResult ArticlesList()
        {
           
            ViewBag.Categories = _db.categories.ToList();
            var result = ArticlesData.AllArticles();
            return View(result);
        }


        //GET: Articles/Details/5
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
            return View("~/Views/Shared/ArticlesSharedViews/_ArticlesInfoSharedView.cshtml", result);
        }




        // GET: Articles/Create
        //public ActionResult CreateArticles()
        //{
        //    ViewBag.Categories = _db.categories.ToList();
        //    ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
        //    ViewBag.Requests = _db.requests.ToList();

        //    var model = new ArticlesCustomClass();
        //    model.Categories = _db.categories.ToList();
        //    model.Requests = _db.requests.ToList();
        //    return View("~/Views/Shared/ArticlesSharedViews/_ArticlesFormsSharedView.cshtml", model);
        //}

        //// POST: Articles/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateArticles(ArticlesCustomClass model, HttpPostedFileBase[] images)
        //{
        //ViewBag.Categories = _db.categories.ToList();
        //ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email");
        //ViewBag.RequestId = new SelectList(_db.requests.ToList(), "Id", "title");
        //ViewBag.Requests = _db.requests.ToList();


        //    if (ModelState.IsValid)
        //    {
        //        ArticlesData.CreateArticles(model, images);

        //        return RedirectToAction("ArticlesList");
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult>  CreateArticles(ArticlesCustomClass model, HttpPostedFileBase[] images)
        //{
        //ViewBag.Categories = _db.categories.ToList();
        //ViewBag.Requests = _db.requests.ToList();

        //if (! ModelState.IsValid) {
        // return View("~/Views/Shared/ArticlesSharedViews/_ArticlesFormsSharedView.cshtml", model);
        //return RedirectToAction("ArticlesList");
        //}
        //ArticlesData.CreateArticles(model, images);
        //    var task = _db.SaveChangesAsync();
        //    await task;

        //if (task.Exception != null)
        //{
        //    ModelState.AddModelError("", "Unable to add the Asset");
        //    return View("~/Views/Shared/ArticlesSharedViews/_ArticlesFormsSharedView.cshtml", model);
        //}
        //return Content("success");
        //return RedirectToAction("ArticlesList");
        //}



        /// GET: Articles/Edit/5
        public ActionResult EditArticles(int id)
        {
            ViewBag.Images = _db.images.Where(e => e.articlesId == id); 
            ViewBag.ImagesCount = _db.images.Where(e => e.articlesId == id).Count();
            var result = ArticlesData.GetArticlesById(id);
            ViewBag.Categories = _db.categories.ToList();   
            //ViewBag.UserId = new SelectList(_db.users.ToList(), "Id", "email", result.usersId);
            ViewBag.Requests = _db.requests.ToList();

            //ამ ცვლადში ვპოულობთ უკვე დამატებულ კატეგორიებს და ვაკონვერტირებთ მასივში
            var SelectedCategories = ArticlesData.GetSelectedCatagories(id).Select(e => e.categoriesId).ToArray();
            TempData["PrevSelectedCategories"] = SelectedCategories;// Post-ის edit-თვის გადასაცემი კატეგორიების 

            //ამ ცვლადში ვპოულობთ უკვე დამატებულ მოთხოვნებს და ვაკონვერტირებთ მასივში          
            List<int> Selectedrequests = ArticlesData.GetSelectedRequest(id).Select(e =>(int)e.requestsId).ToList();         
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

            // შეცვლილი მოდელის დამატება
            if (ModelState.IsValid)
                {
                    ArticlesData.EditArticles(model, images);
                    return RedirectToAction("ArticlesList");
                } 
            return View(model);
        }


         [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditMainImage(int id, int primary)
        {
            var oldMain = _db.images.Where(x => x.articlesId == id && x.isMain == true).FirstOrDefault();

            var newMain = _db.images.Where(x => x.articlesId == id && x.Id == primary).FirstOrDefault();

            if (oldMain != null)
            {
                oldMain.isMain = false;
            }
            if (newMain != null)
            {

                newMain.isMain = true;
            }
            else
            {
                oldMain.isMain = true;

            }          
            _db.SaveChanges();
            return RedirectToAction("ArticlesList");
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
            return RedirectToAction("ArticlesList");
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteImages(int id,int primary)
        {
            //var articleIds = _db.articles.FirstOrDefault(e => e.Id == id);
            var result = _db.images.FirstOrDefault(x => x.articlesId == id && x.Id == primary);
            try
            {
                ArticlesData.DeleteImages(result);
            }
            catch
            {
                return View(result);
            }
            return RedirectToAction("ArticlesList");
        }


        //// GET: Articles/block/5
        //public ActionResult BlockArticle()
        //{
        //    ViewBag.Categories = _db.categories.ToList();
        //    var result = ArticlesData.GetArticlesById(id);
        //    if (result == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(result);
        //}

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult MultiDelete(FormCollection formCollection)
        {
            string[] ids = formCollection["articleId"].Split(new char[] {','});
            int[] idToInts = Array.ConvertAll(ids, s => int.Parse(s));
            ArticlesData.MultiArticleDelete(idToInts);
            return RedirectToAction("ArticlesList");
        }




        //POST: Categories/block/5 
        [HttpPost]      
        public ActionResult BlockArticle(int id)
        {

            try
            {
                ArticlesData.BlockArticle(id);
                return Content(Boolean.TrueString);
            }
            catch
            {
                //return View(model);
                return Content(Boolean.FalseString);
            }
            //return RedirectToAction("ArticlesList");
        }


        //POST: Categories/block/5
        [HttpPost]

        public ActionResult UnBlockArticle(int id)
        {

            try
            {
                ArticlesData.UnBlockArticle(id);
                return Content(Boolean.TrueString);
            }
            catch
            {
                //return View(model);
                return Content(Boolean.FalseString);
            }
            //return RedirectToAction("ArticlesList");
        }

        #endregion

        #region  Categories 

        // GET: Categories

        public ActionResult CategoriesList()
        {
            var result = CategoriesData.AllCategories();
            return View(result);
        }


        // GET: Categories/Details/5
        public ActionResult CategoriesDetails(int id)
        {
            var result = CategoriesData.GetCategoriesById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Categories/Create
        public ActionResult CreateCategories()
        {
            var list = _db.categories.ToList();
            list.Insert(list.Count, new categories() { Id = 0, name = "სხვა" });
            ViewBag.ParentCategoryId = new SelectList(list, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategories(CategoriesCustomClass model)
        {
            //var CategoriesList = _db.categories.ToList();
            //CategoriesList.Insert(0, new categories() { Id = 0, name = "სხვა" });           
            //ViewBag.ParentId = new SelectList(CategoriesList, "Id", "Name");
            if (ModelState.IsValid)
            {
                CategoriesData.CreateCategories(model);
                return RedirectToAction("CategoriesList");
            }
            else
            {

                return View(model);
            }
        }

        //add new category 
        public ActionResult AddNewCategory(string categoryName)
        {
            var categories = _db.categories;
            
                if (categories.Count(e => e.name == categoryName) == 0)
                {
                    _db.categories.Add(new categories() { Id=0, name = categoryName });
                    _db.SaveChanges();
                }               
           
                var list= _db.categories.ToList();
                list.Insert(list.Count, new categories() { Id = 0, name = "სხვა" });
                var result = list.OrderByDescending(e => e.Id).Select(c => new { Id = c.Id, name = c.name });
                return Json(result, JsonRequestBehavior.AllowGet);
        }



        //// GET: Categories/Edit/5
        public ActionResult EditCategories(int id)
        {
            var result = CategoriesData.GetCategoriesById(id);
            var list = _db.categories.ToList();
           // list.Insert(0, new categories() { name = "-- Please Select --" });
            list.Insert(list.Count, new categories() { Id =0, name = "სხვა" });
            ViewBag.ParentCategoryId = new SelectList(list, "Id", "Name", result.parentId);

            var customCategory = new CategoriesCustomClass()
            {
                Name = result.name,
                Id = result.Id,
                ParentId = result.parentId
            };
            return View(customCategory);
        }


        //// POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategories(CategoriesCustomClass model)
        {
            var list = _db.categories.ToList();
            ViewBag.ParentId = new SelectList(list, "Id", "Name");

            if (ModelState.IsValid)
            {
                CategoriesData.EditCategories(model);
                return RedirectToAction("CategoriesList");
            }
            return View(model);
        }




        // GET: Categories/Delete/5
        public ActionResult DeleteCategories(int id)
        {
            var result = CategoriesData.GetCategoriesById(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategories(categories model)
        {
            try
            {
                CategoriesData.FullDeleteCategories(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("CategoriesList");
        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(categories model)
        //{
        //    try
        //    {
        //        UserData.deleteCategories(model);
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //    return RedirectToAction("index");
        //}

        #endregion

        #region Requests
        // GET: Requests

        public ActionResult RequestsList()
        {
            ViewBag.Categories = _db.categories.ToList();
            var result = RequestData.AllRequest();
            return View(result);
        }


        // GET: Articles/Details/5
        public ActionResult RequestsDetails(int id)
        {
            var articleResult = _db.requestsArticles.FirstOrDefault(e => e.requestsId == id);
            var articleIds= _db.articles.Where(e => e.Id == articleResult.articlesId).Count();
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
                return RedirectToAction("RequestsList");
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
                Title= result.title,
                Content = result.content,
                //IsDone = result.isDone,
                //Upvote=result.upvote,   
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
                return RedirectToAction("RequestsList");
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
            return RedirectToAction("RequestsList");
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


        [HttpPost]
        public ActionResult DoneRequest(int id)
        {

            try
            {
                RequestData.DoneRequest(id);
                return Content(Boolean.TrueString);
            }
            catch
            {
                //return View(model);
                return Content(Boolean.FalseString);
            }
            //return RedirectToAction("ArticlesList");
        }


        //POST: Categories/block/5
        [HttpPost]
        public ActionResult UnDoneRequest(int id)
        {
            try
            {
                RequestData.UnDoneRequest(id);
                return Content(Boolean.TrueString);
            }
            catch
            {
                //return View(model);
                return Content(Boolean.FalseString);
            }
            //return RedirectToAction("ArticlesList");
        }


        #endregion

        #region user
        // GET: Users

        public ActionResult UserList()
        {
            var result = UserData.AllUsers();
            return View(result);
        }


        // GET: Users/Details/5
        public ActionResult UserDetails(int id)
        {
            var result = UserData.GetUserById(id);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }


        // GET: Users/Create
        public ActionResult CreateUser()
        {
            //var categories = data.GetUserCategories();
            ViewBag.CategoriesId = new SelectList(_db.usersCategories.ToList(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(UsersCustomClass model)
        {
            ViewBag.CategoriesId = new SelectList(_db.usersCategories.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                UserData.CreateUser(model);
                return RedirectToAction("UserList");
            }
            return View(model);
        }

        //// GET: Users/Edit/5  
        public ActionResult EditUser(int id)
        {
            var result = UserData.GetUserById(id);
            //var categories = data.GetUserCategories();
            ViewBag.CategoriesId = new SelectList(_db.usersCategories.ToList(), "Id", "Name", result.categoriesId);
            var customUser = new UsersCustomeEditClass()
            {
                Name = result.name,
                Surname = result.surname,
                Email = result.email,
                CategoriesId = result.categoriesId,
                IsActive = result.isActive
            };
            return View(customUser);
        }


        //// POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UsersCustomeEditClass model)
        {
            ViewBag.CategoriesId = new SelectList(_db.usersCategories.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                UserData.EditUser(model);
                return RedirectToAction("UserList");
            }
            return View(model);
        }

        // GET: Users/Delete/5
        public ActionResult DeleteUser(int id)
        {
            var result = UserData.GetUserById(id);


            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(users model)
        {
            try
            {
                UserData.deleteUser(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("UserList");
        }

        // GET: Users/fullDelete/5
        public ActionResult fullDeleteUser(int id)
        {
            var result = UserData.GetUserById(id);


            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        //POST: Users/fullDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult fullDeleteUser(users model)
        {
            try
            {
                UserData.FullDeleteUser(model);
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("UserList");
        }

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