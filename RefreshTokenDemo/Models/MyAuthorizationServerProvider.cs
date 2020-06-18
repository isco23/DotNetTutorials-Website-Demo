using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RefreshTokenDemo.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        ClientMaster2 client;
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            if(!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.SetError("Invalid_client", "Client Credentials could not be retrieved through the Authorization Header.");
                return Task.FromResult<object>(null);
            }
            ClientMaster2 client = (new ClientMasterRepository()).ValidateClient(clientId, clientSecret);
            if(client == null)
            {
                context.SetError("Invalid_client", "Client Credentials are invalid.");
                return Task.FromResult<object>(null);
            }
            else
            {
                if (!client.Active)
                {
                    context.SetError("Invalid_client", "Client is Inactive.");
                    return Task.FromResult<object>(null);
                }

                context.OwinContext.Set<ClientMaster2>("ta:client", client);
                context.OwinContext.Set<string>("ta:clientAllowedOrigin", client.AllowedOrigin);
                context.OwinContext.Set<string>("ta:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());
                context.Validated();
                return Task.FromResult<object>(null);
            }
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ClientMaster2 client = context.OwinContext.Get<ClientMaster2>("ta:client");
            var allowedOrigin = context.OwinContext.Get<string>("ta:clientAllowedOrigin");
            if(allowedOrigin == null)
            {
                allowedOrigin = "*";
            }
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            UserMaster user = null;
            using(UserMasterRepository _repo = new UserMasterRepository())
            {
                user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided Username and password is incorrect");
                    return;
                }
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRoles));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("Email", user.UserEmailID));

            var props = new AuthenticationProperties(new Dictionary<string,string>
            {
                {
                    "client_id",(context.ClientId == null) ? string.Empty : context.ClientId
                },
                {
                    "userName",context.UserName
                }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string,string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var currentClient = context.ClientId;
            if(originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId");
                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
    }
}