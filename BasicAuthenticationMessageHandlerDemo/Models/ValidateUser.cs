using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthenticationMessageHandlerDemo.Models
{
    public class ValidateUser
    {
        public UserMaster CheckUserCredentials(string username, string password)
        {
            using (var context = new UsersDBEntities())
            {
                return context.UserMasters.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.UserPassword == password);
            }
        }
    }
}