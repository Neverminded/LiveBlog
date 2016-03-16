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
    public class MailController : Controller
    {

        private DataManager _datamanager;

        public MailController(DataManager datamanager)
        {
            _datamanager = datamanager;

        }

        public ActionResult Index()
        {
            return View("Result");
        }
        public ActionResult getMailsresult()
        {

            ViewData["results"] = _datamanager.Mail.GetAllMails();
            return View("getMailsresult");
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMail(int id)
        {
            ViewData.Model = _datamanager.Mail.GetMail(id);
            return View();

        }


        public ActionResult DeleteMail(int id)
        {

            if (_datamanager.Mail.DeleteMailRecord(id))
            {
                return RedirectToAction("getMailsresult");
            }

            return View("getMailsresult");

        }


        [HttpPost]
        public ActionResult MailSubmit(MailResults model)
        {
            if (Request.IsAjaxRequest() && ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.NameSender))
                {
                    ModelState.AddModelError("NameSender", "Некорректное название книги");
                }

                _datamanager.Mail.CreateMailRecord(model.NameSender, model.EMailSender, model.TextSender, model.ThemeMail);
                ViewData["mails"] = _datamanager.Mail.GetAllMails();

                return Content("Ваше обращение успешно отправлено автору сайта! Спасибо!");
            }
            ModelState.AddModelError("NameSender", "dsfdgИмя пользователя или пароль указаны неверно.");
            return View("Result", model);



        }

    }
}
