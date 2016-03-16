using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.SearchEngine
{
    public class RecordSearch : SearchItem
    {
        Record Model;
        public RecordSearch(Record model)
        {
            this.Model = model;
        }
        public override int Id
        {
            get { return this.Model.Id_Record; }
        }
        [DataType(DataType.MultilineText)]
        public override string Title
        {
            get { return Model.Title + "..."; }

        }



        public override string CssClass
        {
            get { return "recordsearch"; }
        }

        public override string TypeName
        {
            get { return "Статья:"; }

        }


    }
}