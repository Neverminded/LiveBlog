using MyBlog.Core;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    [CategoriesFilter]
    public class ResumeController : Controller
    {
        private DataManager _datamanager;

        public ResumeController(DataManager datamanager)
        {
            _datamanager = datamanager;

        }


        public ActionResult Index()
        {
            if (System.IO.File.Exists(Request.PhysicalApplicationPath + "/Content/rezume.txt"))
            {
                StreamReader streamReader = new StreamReader(Request.PhysicalApplicationPath + "/Content/rezume.txt"); //Открываем файл для чтения

                string str = ""; //Объявляем переменную, в которую будем записывать текст из файла

                while (!streamReader.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
                {
                    str += streamReader.ReadLine(); //В переменную str по строчно записываем содержимое файла
                }
                ViewData["textrezume"] = str;
                streamReader.Close();
                return View();
            }

            else return RedirectToAction("GetRecords", "Record");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddResume()
        {
            return View();


        }



        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult AddResume(string Title)
        {
            string path = Request.PhysicalApplicationPath + "/Content/rezume.txt";
            FileInfo file = new FileInfo(path);
            if (file.Exists == false) //Если файл не существует
            {
                //file.Create(); //Создаем
                StreamWriter write_text;  //Класс для записи в файл

                write_text = file.AppendText(); //Дописываем инфу в файл, если файла не существует он создастся
                write_text.WriteLine(Request.Form["textar3"]); //Записываем в файл текст из текстового поля
                write_text.Close(); // Закрываем файл
                return RedirectToAction("Index");
            }
            else return RedirectToAction("GetRecords", "Record");

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditResume()
        {
            if (System.IO.File.Exists(Request.PhysicalApplicationPath + "/Content/rezume.txt"))
            {
                StreamReader streamReader = new StreamReader(Request.PhysicalApplicationPath + "/Content/rezume.txt"); //Открываем файл для чтения

                string str = ""; //Объявляем переменную, в которую будем записывать текст из файла

                while (!streamReader.EndOfStream) //Цикл длиться пока не будет достигнут конец файла
                {
                    str += streamReader.ReadLine(); //В переменную str по строчно записываем содержимое файла
                }
                streamReader.Close();
                ViewData["textrezume"] = str;
                return View();
            }

            else return RedirectToAction("GetRecords", "Record");


        }



        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditResume(string Title)
        {
            string path = Request.PhysicalApplicationPath + "/Content/rezume.txt";
            if (System.IO.File.Exists(path))
            {

                FileInfo file = new FileInfo(path);
                file.Delete();
                StreamWriter write_text;  //Класс для записи в файл

                write_text = file.AppendText(); //Дописываем инфу в файл, если файла не существует он создастся
                write_text.WriteLine(Request.Form["textar3"]); //Записываем в файл текст из текстового поля
                write_text.Close(); // Закрываем файл
                return RedirectToAction("Index");


            }
            else return RedirectToAction("GetRecords", "Record");

        }








    }
}
