using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class CommentRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;
        public CommentRepository(MyDBObjectsLINQDataContext datacontext)
        {
            _datacontext = datacontext;

        }

        //возвращает все комментарии по указанной статье
        public IEnumerable<Comment> GetCommentsForRecord(int id)
        {
            return _datacontext.Comment.Where(c => c.IdRecord == id).OrderByDescending(c => c.DateCreate);

        }

        public Comment GetComment(int id)
        {
            return _datacontext.Comment.SingleOrDefault(c => c.Id_Comment == id);

        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _datacontext.Comment.OrderBy(x => x.Text);
        }

        //разобраться с ид юзера (Guid)
        //добавление коммента
        public Comment CreateComment(int idrecord, string text)
        {

            Comment cmnt = new Comment { Text = text, DateCreate = DateTime.Now, IdRecord = idrecord, NameAuthorComment = HttpContext.Current.User.Identity.Name };
            _datacontext.Comment.InsertOnSubmit(cmnt);
            _datacontext.SubmitChanges();
            return cmnt;

        }

        //обновление коммента
        public void UpdateComment(Comment cm)
        {

            Comment cmnt = GetComment(cm.Id_Comment);
            cmnt.Text = cm.Text;
            _datacontext.SubmitChanges();

        }

        //удаление коммента
        public void DeleteComment(int idComment)
        {

            Comment cmnt = GetComment(idComment);
            _datacontext.Comment.DeleteOnSubmit(cmnt);
            _datacontext.SubmitChanges();

        }



    }
}