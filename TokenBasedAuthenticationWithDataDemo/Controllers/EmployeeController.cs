using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenBasedAuthenticationWithDataDemo.Models;

namespace TokenBasedAuthenticationWithDataDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeRepository repo = new EmployeeRepository();
        [Authorize]
        [HttpGet]
        [Route("api/employee/getall")]
        public IHttpActionResult GetAllEmployees()
        {
            return Ok(repo.GetAllEmployee());
        }
    }
}
