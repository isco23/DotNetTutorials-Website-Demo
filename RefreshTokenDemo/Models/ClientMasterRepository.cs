using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefreshTokenDemo.Models
{
    public class ClientMasterRepository : IDisposable
    {
        UsersDBEntities context = new UsersDBEntities();
        public void Dispose()
        {
            context.Dispose();
        }

        public ClientMaster2 ValidateClient(string ClientID, string ClientSecret)
        {
            return context.ClientMaster2.FirstOrDefault(user => user.ClientID == ClientID && user.ClientSecret == ClientSecret);
        }
    }
}