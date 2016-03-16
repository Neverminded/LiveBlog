using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Models
{
    public class MailRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;
        public MailRepository(MyDBObjectsLINQDataContext datacontext)
        {

            _datacontext = datacontext;

        }
        public IEnumerable<MailResults> GetAllMails()
        {

            return _datacontext.MailResults.OrderBy(r => r.TimeSending);

        }

        [ValidateInput(false)]
        public MailResults CreateMailRecord(string nameusersender, string emailuser, string mailarea, string themeMail)
        {

            MailResults rec1 = new MailResults { NameSender = nameusersender, EMailSender = emailuser, TextSender = mailarea, ThemeMail = themeMail, TimeSending = DateTime.Now };
            _datacontext.MailResults.InsertOnSubmit(rec1);
            _datacontext.SubmitChanges();
            return rec1;
        }


        public MailResults GetMail(int id)
        {
            return _datacontext.MailResults.SingleOrDefault(t => t.Id_mailRecord == id);

        }


        public bool DeleteMailRecord(int recId)
        {

            MailResults rec = _datacontext.MailResults.First(r => r.Id_mailRecord == recId);
            _datacontext.MailResults.DeleteOnSubmit(rec);
            _datacontext.SubmitChanges();
            return true;

        }





    }
}