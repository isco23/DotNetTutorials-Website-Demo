using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationWithDataDemo.Models
{
    public class ClientMasterRepository : IDisposable
    {
        UsersDBEntities context = new UsersDBEntities();
        public void Dispose()
        {
            context.Dispose();
        }

        public ClientMaster ValidateClient(string ClientID, string ClientSecret)
        {
            return context.ClientMasters.FirstOrDefault(user => user.ClientId == ClientID && user.ClientSecret == ClientSecret);
        }
    }
}