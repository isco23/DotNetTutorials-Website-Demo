using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace BasicAuthenticationMessageHandlerDemo.Controllers
{
    public class TestController : ApiController
    {
        [Authorize(Roles = "Admin,User")]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, username);
        }
        
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage Post()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            return Request.CreateResponse(HttpStatusCode.OK, username);
        }
    }
}
