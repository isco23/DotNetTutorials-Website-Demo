using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ConsumeApiJavaScriptDemo.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Role()
        {
            using(var client = new HttpClient())
            {                
                client.DefaultRequestHeaders.Add("Authorization", "Basic Tm9ybWFsVXNlcjpub3JtYWx1c2Vy");
                HttpResponseMessage response = client.GetAsync("http://localhost:50527/api/AllMaleEmployees").Result;                               
                if (response != null)
                {
                    return RedirectToAction("Home", "Check");
                }
            }
            return View();
        }

        public ViewResult Check()
        {
            return View();
        }
    }
}