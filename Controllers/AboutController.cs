using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Core;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class AboutController : Controller
    {
        private DataManager _datamanager;

        public AboutController(DataManager datamanager)
        {
            _datamanager = datamanager;
        }


        public ActionResult About()
        {
            return View();
        }

    }
}
