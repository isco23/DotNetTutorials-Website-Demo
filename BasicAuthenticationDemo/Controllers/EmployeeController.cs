﻿using BasicAuthenticationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace BasicAuthenticationDemo.Controllers
{
    public class EmployeeController : ApiController
    {
        [BasicAuthentication]
        [EnableCorsAttribute("*", "*", "*")]
        public HttpResponseMessage GetEmployees()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            
            var EmpList = new EmployeeBL().GetEmployees();
            switch (username.ToLower())
            {
                case "maleuser":
                    return Request.CreateResponse(HttpStatusCode.OK, EmpList.Where(x => x.Gender.ToLower() == "male").ToList());
                case "femaleuser":
                    return Request.CreateResponse(HttpStatusCode.OK, EmpList.Where(e => e.Gender.ToLower() == "female").ToList());
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
