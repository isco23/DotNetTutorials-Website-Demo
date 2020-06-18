using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefreshTokenDemo.Models
{
    public class UserTokenRepository : IDisposable
    {
        UsersDBEntities context = new UsersDBEntities();
        public void Dispose()
        {
            context.Dispose();
        }

        public Token GetTokenFromDb(string username , string password)
        {
            UserTokenMaster userMaster = context.UserTokenMasters.FirstOrDefault(user => user.UserName.Equals(username,StringComparison.OrdinalIgnoreCase) && user.UserPassword == password);
            Token token = null;
            if(userMaster != null)
            {
                token = new Token()
                {
                    AccessToken = userMaster.AccessToken,
                    RefreshToken = userMaster.RefreshToken,
                    ExpiredDateTime = (DateTime)userMaster.TokenExpiredTime
                };
            }
            return token;
        }

        public bool AddUserTokenIntoDb(UserTokenMaster userTokenMaster)
        {
            var tokenMaster = context.UserTokenMasters.FirstOrDefault(x => x.UserName == userTokenMaster.UserName && x.UserPassword == userTokenMaster.UserPassword);
            if(tokenMaster != null)
            {
                context.UserTokenMasters.Remove(tokenMaster);
            }
            context.UserTokenMasters.Add(userTokenMaster);
            bool isAdded = context.SaveChanges() > 0;
            return isAdded;
        }
    }
}