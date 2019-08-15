using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowToWebApplication.Models
{
    public class CategoriesDataProvider
    {
        HowToDbEntities _db = new HowToDbEntities();


        public bool Exist(CategoriesCustomClass category)
        {
            return _db.categories.FirstOrDefault(e => e.name == category.Name) == null ? false : true;
        }

      
        public categories GetCategoriesById(int id)
        {
            return _db.categories.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateCategories(CategoriesCustomClass category)
        {
            if (!Exist(category) )
            {
                _db.categories.Add(new categories()
                {
                    name = category.Name,
                    //Id = category.Id,
                    parentId=category.ParentId
                });
            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditCategories(CategoriesCustomClass category)
        {
            var result = _db.categories.FirstOrDefault(e => e.Id == category.Id);
            if (!Exist(category) || result.name == category.Name)
            {
                result.name = category.Name;
                result.parentId = category.ParentId;
            }
            _db.SaveChanges();
        }


        //Delete
        //public void deleteCategories(Users user)
        //{
        //    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
        //    result.IsActive = false;
        //    _db.SaveChanges();
        //}


        public void FullDeleteCategories(categories category)
     

        {   var result = _db.articleCategories.Where(e => e.categoriesId == category.Id).ToList();

            var deleteImage = _db.images.Where(i => result.Any(j => j.articlesId == i.articlesId)); 
            var deleteFavourite = _db.favourites.Where(i => result.Any(j => j.articlesId == i.articlesId));
            var deleteRating = _db.ratings.Where(i => result.Any(j => j.articlesId == i.articlesId));
            var deleteArticleTags = _db.articlesTags.Where(i => result.Any(j => j.articlesId == i.articlesId));
            var deleteArticle = _db.articles.Where(i => result.Any(j => j.articlesId == i.Id));
            var deleteArticleCategories = _db.articleCategories.Where(e => e.categoriesId == category.Id);
            var deletecategory = _db.categories.Where(e => e.Id == category.Id);



         
            _db.images.RemoveRange(deleteImage);
            _db.favourites.RemoveRange(deleteFavourite);
            _db.ratings.RemoveRange(deleteRating);
            _db.articlesTags.RemoveRange(deleteArticleTags);
            _db.articles.RemoveRange(deleteArticle);
            _db.articleCategories.RemoveRange(deleteArticleCategories);
            _db.categories.RemoveRange(deletecategory);
            _db.SaveChanges();
        }
    
        ////search 
        //public IEnumerable<categories> GetCategories(string name)
        //{
        //    return _db.categories.Where(e => e.name.Contains(name));
        //}


        public IEnumerable<categories> AllCategories()
        {
            return _db.categories;
        }
    }
}