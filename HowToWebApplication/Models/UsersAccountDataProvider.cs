using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HowToWebApplication.Helpers;
using static HowToWebApplication.Helpers.PasswordHelper;

namespace HowToWebApplication.Models
{
    public class UsersAccountDataProvider
    {
        HowToDbEntities _db = new HowToDbEntities();
        public void AddUser(users user)
        {
            _db.users.Add(user);
            _db.SaveChanges();
        }
        //public bool ValidLogin(LoginViewModel user)
        //{
        //    var pass = SHA.GenerateSHA512String(user.Password);
        //    var result = _db.users.FirstOrDefault(e => e.email == user.Email && e.password == pass);
        //    return result == null ? false : true;

        //}
        public bool ValidLoginAdmin(LoginViewModel user)
        {

            var pass = SHA.GenerateSHA512String(user.Password);
            var result = _db.users.FirstOrDefault(e => e.email == user.Email.Trim() && e.password == pass && e.usersCategories.Id == 1);

            return result == null ? false : true;
        }
            public bool ValidLoginModer(LoginViewModel user)
            {

                var pass = SHA.GenerateSHA512String(user.Password);
                var result = _db.users.FirstOrDefault(e => e.email == user.Email && e.password == pass && e.usersCategories.Id == 2);

                return result == null ? false : true;
            }
        

        public bool ValidLoginUser(LoginViewModel user)
        {

            var pass = SHA.GenerateSHA512String(user.Password);
            var result = _db.users.FirstOrDefault(e => e.email == user.Email && e.password == pass && e.usersCategories.Id == 3);

            return result == null ? false : true;
        }


        public users LoginCategory(LoginViewModel user)
        {
            var pass = SHA.GenerateSHA512String(user.Password);
            var result = _db.users.FirstOrDefault(e => e.email == user.Email && e.password == pass);   
            return result;
        }
    }
}