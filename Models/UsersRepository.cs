using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyBlog.Models
{
    public class UsersRepository
    {
        private MyDBObjectsLINQDataContext _datacontext;
        public UsersRepository(MyDBObjectsLINQDataContext datacontext)
        {

            _datacontext = datacontext;

        }

        public IEnumerable<UserProfile> GetAllUsers()
        {

            return _datacontext.UserProfile.OrderBy(r => r.UserName);

        }
        public UserProfile GetUser(int id)
        {
            return _datacontext.UserProfile.SingleOrDefault(t => t.UserId == id);

        }

        public webpages_Membership GetMembership(int id)
        {
            return _datacontext.webpages_Membership.SingleOrDefault(t => t.UserId == id);

        }


        public webpages_UsersInRoles GetUserRole(int id)
        {
            return _datacontext.webpages_UsersInRoles.SingleOrDefault(t => t.UserId == id);

        }

        public bool DeleteUser(int userId)
        {
            UserProfile usr = GetUser(userId);
            _datacontext.UserProfile.DeleteOnSubmit(usr);
            webpages_Membership member = GetMembership(userId);
            _datacontext.webpages_Membership.DeleteOnSubmit(member);
            webpages_UsersInRoles userRole = GetUserRole(userId);
            if(userRole!=null)
            {
            _datacontext.webpages_UsersInRoles.DeleteOnSubmit(userRole);
                }
            member = GetMembership(userId);
            _datacontext.webpages_Membership.DeleteOnSubmit(member);
            _datacontext.SubmitChanges();
            return true;
        }




    }
}