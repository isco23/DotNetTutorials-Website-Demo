using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationWithDataDemo.Models
{
    public class UserMasterRepository : IDisposable
    {
        UsersDBEntities context = new UsersDBEntities();
        public void Dispose()
        {
            context.Dispose();
        }

        public UserMaster ValidateUser(string username, string password)
        {
            return context.UserMasters.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.UserPassword == password);

        }
    }
}