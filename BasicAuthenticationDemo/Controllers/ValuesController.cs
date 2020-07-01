using BasicAuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;

namespace BasicAuthenticationDemo.Controllers
{
    //[Authorize]
    [BasicAuthentication]
    public class ValuesController : ApiController
    {
        public string Get()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return "values";
            }
            return "false";
        }
    }
}
