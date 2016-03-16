using MyBlog.Core;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class CommentController : Controller
    {
        private DataManager _datamanager;

        public CommentController(DataManager datamanager)
        {
            _datamanager = datamanager;

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteComment(int id, int idrec)
        {
            if (Request.IsAjaxRequest())
            {
                _datamanager.Comment.DeleteComment(id);
                ViewData["comments"] = _datamanager.Comment.GetCommentsForRecord(idrec);
                return PartialView("_commentsview");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
