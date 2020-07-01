using RoleBasedBasicAuthenticationDemo.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace RoleBasedBasicAuthenticationDemo.Controllers
{
    [BasicAuthentication]
    [EnableCorsAttribute("*", "*", "*")]
    public class EmployeeController : ApiController
    {        
        [MyAuthorize]
        [Route("api/AllMaleEmployees")]        
        public HttpResponseMessage GetAllMaleEmployees()
        {
            if (CheckRole.CheckRoleForNormalUser())
            {
                var EmpList = new EmployeeBL().GetEmployees().Where(x => x.Gender.ToLower() == "male").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, EmpList);
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"You have no access!");
        }

        [MyAuthorize(Roles = "SuperAdmin")]
        [Route("api/AllFemaleEmployees")]
        public HttpResponseMessage GetAllFemaleEmployees()
        {
            var EmpList = new EmployeeBL().GetEmployees().Where(x => x.Gender.ToLower() == "female").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }

        [MyAuthorize(Roles = "Admin,SuperAdmin")]
        [Route("api/AllEmployees")]
        public HttpResponseMessage GetAllEmployees()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var Id = identity.Claims.FirstOrDefault(a => a.Type == "ID").Value;
            var Email = identity.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            var username = identity.Name;
            var EmpList = new EmployeeBL().GetEmployees();
            return Request.CreateResponse(HttpStatusCode.OK, EmpList);
        }
    }
}
