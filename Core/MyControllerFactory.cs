using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MyBlog.Models;
using System.Web.Routing;

namespace MyBlog.Core
{
    public class MyControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbBlogConnectionString"].ConnectionString;

            return Activator.CreateInstance(controllerType, new DataManager(connectionString)) as IController;


        }


    }
}