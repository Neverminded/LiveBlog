using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class MembershipRepository
    {

        private MyDBObjectsLINQDataContext _datacontext;
        public MembershipRepository(MyDBObjectsLINQDataContext datacontext)
        {

            _datacontext = datacontext;

        }

        public bool DeleteMembership(int userId)
        {
            /*
            UserProfile usr = GetUser(userId);

            _datacontext.UserProfile.DeleteOnSubmit(usr);
            _datacontext.SubmitChanges();
             * */
            return true;
        }

    }
}