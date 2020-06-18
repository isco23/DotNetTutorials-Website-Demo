using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TokenBasedAuthenticationWithDataDemo.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        ClientMaster client;
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //string clientId = string.Empty;
            //string clientSecret = string.Empty;
            //if(!context.TryGetBasicCredentials(out clientId , out clientSecret))
            //{
            //    context.SetError("invalid_client", "Client credentials could not be retrieved through th e Authorization header");
            //    context.Rejected();
            //    return;
            //}

            //client = (new ClientMasterRepository()).ValidateClient(clientId, clientSecret);
            //if(client != null)
            //{                
            //    context.OwinContext.Set<ClientMaster>("oauth:client", client);
            //    context.Validated(clientId);
            //}
            //else
            //{
            //    context.SetError("invalid_client", "Client credentials are invalid");
            //    context.Rejected();
            //}            
            context.Validated();

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepository _repo = new UserMasterRepository())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");

                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoles));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                //identity.AddClaim(new Claim("ClientName", client.ClientName));
                context.Validated(identity);
            }
        }
    }
}