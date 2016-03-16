using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;

namespace MyBlog.Models
{
    public class CategoryRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;

        public CategoryRepository(MyDBObjectsLINQDataContext datacontext)
        {
            _datacontext = datacontext;
        }

        public IEnumerable<Categories> GetAllCategories()
        {

            return _datacontext.Categories.OrderBy(r => r.NameCategory);
        }


        public Categories CreateCategory(string name)
        {

            Categories cat = new Categories { NameCategory = name };
            _datacontext.Categories.InsertOnSubmit(cat);
            _datacontext.SubmitChanges();
            return cat;
        }


        public Categories GetCategory(int id)
        {
            return _datacontext.Categories.SingleOrDefault(t => t.Id_Category == id);

        }


        public bool Deletecategory(int catId)
        {
            Categories cat = GetCategory(catId);
            _datacontext.Categories.DeleteOnSubmit(cat);
            _datacontext.SubmitChanges();
            return true;
        }










    }
}