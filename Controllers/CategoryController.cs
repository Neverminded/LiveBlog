using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Core;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class CategoryController : Controller
    {
        private DataManager _datamanager;

        public CategoryController(DataManager datamanager)
        {

            _datamanager = datamanager;
        }

        public ActionResult GetCategories()
        {
            ViewData["Title"] = "Категории";
            ViewData["list"] = _datamanager.Category.GetAllCategories();
            ViewData["Action"] = "GetCategories";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CreateCategory()
        {


            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateCategory(string NameCategory)
        {

            _datamanager.Category.CreateCategory(NameCategory);

            return RedirectToAction("GetCategories");
        }

        public ActionResult FilterRecordsFromCategory(int id)
        {

            ViewData["Title"] = "MyTitle";
            ViewData["list"] = _datamanager.Record.GetRecordsFromCategory(id);
            ViewData["Action"] = "FilterRecordsFromCategory";
            RecordIdForComment r = new RecordIdForComment();
            ViewData["commentCount"] = r.recordforcomment((IEnumerable<Record>)ViewData["list"]);
            ViewData["CategoryTitle"] = _datamanager.Category.GetCategory(id).NameCategory;
            return View();

        }


    }
}
