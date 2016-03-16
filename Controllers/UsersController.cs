using MyBlog.Core;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class UsersController : Controller
    {
        private DataManager _datamanager;

        public UsersController(DataManager datamanager)
        {
            _datamanager = datamanager;
        }

        public ActionResult GetUsers()
        {

            ViewData["Users"] = _datamanager.Users.GetAllUsers();
            return View();
        }


        public ActionResult DeleteUser(int id)
        {

            if (_datamanager.Users.DeleteUser(id))
            {
                return RedirectToAction("GetUsers");
            }

            return View("Index", "Home");

        }


    }
}
