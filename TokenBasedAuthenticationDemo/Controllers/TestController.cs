using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace TokenBasedAuthenticationDemo.Controllers
{
    public class TestController : ApiController
    {
        [Authorize(Roles = "SuperAdmin,Admin,User")]
        [HttpGet]
        [Route("api/test/resource1")]
        public IHttpActionResult GetResource1()
        {
            var identity = (ClaimsIdentity)User.Identity;
            //var ClientName = identity.Claims.FirstOrDefault(c => c.Type == "ClientName").Value;
            //return Ok("Hello: " + identity.Name + ", Client Name : " + ClientName);
            return Ok();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        [Route("api/test/resource2")]
        public IHttpActionResult GetResource2()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var Email = identity.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            var UserName = identity.Name;
            return Ok("Hello " + UserName + ", Your Email ID is :" + Email);
        }

    }
}
