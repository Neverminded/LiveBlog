using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MyBlog.Models
{
    public class RecordRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;


        public RecordRepository(MyDBObjectsLINQDataContext datacontext)
        {

            _datacontext = datacontext;

        }


        //возвращает все записи
        public IEnumerable<Record> GetAllRecords()
        {

            return _datacontext.Record.OrderBy(r => r.DateEdit);

        }

        public static string CutLongText(string value)
        {


            return HttpUtility.HtmlDecode(value.Substring(0, 200) + "...");


        }

        public IEnumerable<Record> GetRecordsFromCategory(int id)
        {

            return _datacontext.Record.Where(r => r.Category_Id == id);

        }

        //добавление новой статьи
        public Record CreateRecord(string title, string text, string textprev, int Id_Category)
        {

            Record rec = new Record { DateCreate = DateTime.Now, Title = title, Text = text, PreviewText = textprev, Category_Id = Id_Category };
            _datacontext.Record.InsertOnSubmit(rec);
            _datacontext.SubmitChanges();
            return rec;
        }

        //выбираем одну статью по id
        public Record GetRecord(int id)
        {
            return _datacontext.Record.SingleOrDefault(t => t.Id_Record == id);

        }

        //обновление статьи
        public void UpdateRecord(Record Myrec)
        {
            Record dbrec = GetRecord(Myrec.Id_Record);
            dbrec.DateEdit = DateTime.Now;
            dbrec.Title = Myrec.Title;
            dbrec.Text = Myrec.Text;
            dbrec.PreviewText = Myrec.PreviewText;

            _datacontext.SubmitChanges();


        }

        //удаление статьи
        public bool DeleteRecord(int recId)
        {
            Record rec = GetRecord(recId);
            _datacontext.Record.DeleteOnSubmit(rec);
            _datacontext.SubmitChanges();
            return true;
        }














    }
}