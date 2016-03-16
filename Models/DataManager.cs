using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class DataManager
    {
        private MyDBObjectsLINQDataContext _datacontext;

        public DataManager(string connectionstring)
        {
            _datacontext = new MyDBObjectsLINQDataContext(connectionstring);

        }

        //поле и свойство для класса юзера
        private UsersRepository _usersrepository;
        public UsersRepository Users
        {
            get
            {
                if (_usersrepository == null)
                    _usersrepository = new UsersRepository(_datacontext);
                return _usersrepository;
            }

        }
        /*
        private webpages_Membership _membershiprepository;
        public webpages_Membership MembershipUsers
        {
            get
            {
                if (_membershiprepository == null)
                    _membershiprepository = new webpages_Membership(_datacontext);
                return _membershiprepository;
            }

        }
        */
        //поле и свойство для класса статьи
        private RecordRepository _recordrepository;
        public RecordRepository Record
        {
            get
            {
                if (_recordrepository == null)
                    _recordrepository = new RecordRepository(_datacontext);
                return _recordrepository;

            }

        }

        //поле и свойство для класа коммента
        private CommentRepository _commentrepository;
        public CommentRepository Comment
        {
            get
            {
                if (_commentrepository == null)
                    _commentrepository = new CommentRepository(_datacontext);
                return _commentrepository;


            }

        }


        private CategoryRepository _categoryrepository;
        public CategoryRepository Category
        {
            get
            {
                if (_categoryrepository == null)
                    _categoryrepository = new CategoryRepository(_datacontext);
                return _categoryrepository;

            }

        }

        private MailRepository _mailrepository;
        public MailRepository Mail
        {
            get
            {
                if (_mailrepository == null)
                    _mailrepository = new MailRepository(_datacontext);
                return _mailrepository;
            }

        }









    }
}