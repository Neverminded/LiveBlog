using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MyBlog.Core
{
    public class RecordIdForComment
    {
        private DataManager _datamanager;
        private int idRecord;
        private int countComment;
        public int IdRecord
        {
            get { return idRecord; }
            set { idRecord = value; }
        }

        public int CountComment { get { return countComment; } set { countComment = value; } }

        public RecordIdForComment()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbBlogConnectionString"].ConnectionString;
            _datamanager = new DataManager(connectionString);
        }

        public List<RecordIdForComment> recordforcomment(IEnumerable<Record> recordcollection)
        {
            List<RecordIdForComment> lst = new List<RecordIdForComment>();
            foreach (var item in recordcollection)
            {
                RecordIdForComment r2 = new RecordIdForComment();
                r2.IdRecord = item.Id_Record;
                r2.CountComment = (_datamanager.Comment.GetCommentsForRecord(item.Id_Record)).Count();
                lst.Add(r2);
            }
            return lst;
        }







    }
}