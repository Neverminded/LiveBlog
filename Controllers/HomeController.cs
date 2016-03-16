using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog.Core;
using MyBlog.SearchEngine;
using System.ServiceModel.Syndication;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class HomeController : Controller
    {
        //Реализуем паттерн DependencyInjection (впрыскивание в класс-контроллер 
        //обьекта для работы с классами данных(репозиториями))
        //1 - создаем поле
        //2- создаем конструктор и присваиваем в поле сылку на класс данных
        private DataManager _datamanager;

        public HomeController(DataManager datamanager)
        {
            _datamanager = datamanager;

        }

        //////////////////////////////////////////////////

        public ActionResult Index()
        {

            ViewData["Title"] = "MyTitle";
            ViewData["list"] = _datamanager.Record.GetAllRecords();
            RecordIdForComment r = new RecordIdForComment();
            ViewData["commentCount"] = r.recordforcomment((IEnumerable<Record>)ViewData["list"]);

            return View();
        }


        public ActionResult StartPage()
        {


            return View();
        }


        public ActionResult Search(string term)
        {
            List<SearchItem> result = new List<SearchItem>();
            var recordresults = _datamanager.Record.GetAllRecords().Where(x => x.Title.Contains(term) || x.Text.Contains(term))
                .Take(5)
                .ToList()
                .Select(x => new RecordSearch(x));

            if (recordresults.Any())
            {


                result.AddRange(recordresults);
            }



            /*
            List<String> mas = new List<string> { "asd", "easdfs","sdff","sdfsdhgjgh" };
            var models = mas.Where(a => a.ToString().Contains(term))
                            .Select(a => new { value = a.ToString() })
                            .Distinct();
            */
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Rss()
        {
            SyndicationFeed feed = new SyndicationFeed("Лента статей", "Читайте мои новые статьи!", new Uri(Request.Url.AbsoluteUri));
            feed.Items = (from t in _datamanager.Record.GetAllRecords()
                          orderby t.DateCreate
                          select
                          new SyndicationItem(t.Title, t.Text, new Uri(Request.Url.AbsoluteUri))
                            );
            return new RssViewResult(feed);
        }




    }
}
