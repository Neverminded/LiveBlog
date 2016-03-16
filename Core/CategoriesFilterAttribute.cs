using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
using System.Web.Mvc;
using System.Configuration;

namespace MyBlog.Core
{
    public class CategoriesFilterAttribute : ActionFilterAttribute
    {

        public string conn = ConfigurationManager.ConnectionStrings["DbBlogConnectionString"].ConnectionString;



        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DataManager dm = new DataManager(conn);
            filterContext.Controller.ViewData["categories"] = dm.Category.GetAllCategories();

        }
    }
}