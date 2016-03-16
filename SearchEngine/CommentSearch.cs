using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.SearchEngine
{
    public class CommentSearch : SearchItem
    {
        Comment Model;
        public CommentSearch(Comment model)
        {
            this.Model = model;
        }
        public override int Id
        {
            get { return (int)this.Model.IdRecord; }
        }



        public override string CssClass
        {
            get { return "commentsearch"; }
        }

        public override string Title
        {
            get { return Model.Text; }

        }

        public override string TypeName
        {
            get { return "Комментарий:"; }
        }
    }
}