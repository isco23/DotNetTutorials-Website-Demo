using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BasicAuthenticationMessageHandlerDemo.Models
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
			try
			{
				var authenticationToken = request.Headers.GetValues("Authorization").FirstOrDefault();
				if(authenticationToken != null)
				{
					byte[] data = Convert.FromBase64String(authenticationToken);
					string decodedAuthenticationToken = Encoding.UTF8.GetString(data);
					string[] UsernamePasswordArray = decodedAuthenticationToken.Split(':');
					string username = UsernamePasswordArray[0];
					string password = UsernamePasswordArray[1];
					UserMaster objUser = new ValidateUser().CheckUserCredentials(username, password);
					if(objUser != null)
					{
						var identity = new GenericIdentity(objUser.UserName);
						identity.AddClaim(new System.Security.Claims.Claim("Email", objUser.UserEmailID));
						IPrincipal principal = new GenericPrincipal(identity, objUser.UserRoles.Split(','));
						Thread.CurrentPrincipal = principal;

						if(HttpContext.Current != null)
						{
							HttpContext.Current.User = principal;
						}
						return base.SendAsync(request, cancellationToken);
					}
					else
					{
						var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
						var tsc = new TaskCompletionSource<HttpResponseMessage>();
						tsc.SetResult(response);
						return tsc.Task;
					}
				}
				else
				{
					var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
					var tsc = new TaskCompletionSource<HttpResponseMessage>();
					tsc.SetResult(response);
					return tsc.Task;
				}
			}
			catch
			{
				var response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
				var tsc = new TaskCompletionSource<HttpResponseMessage>();
				tsc.SetResult(response);
				return tsc.Task;
			}
        }
    }
}