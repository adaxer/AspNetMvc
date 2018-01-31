using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloMvcNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public string Info()
        public ActionResult Info()
        {
            var weekdays = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames;
            return View(weekdays);
            //return new ContentResult { Content = "Hello Mvc" };
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


    }
}