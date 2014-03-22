using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Protal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎来到wz的个人博客";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "没事就来说两句。";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "QQ：418505093。";

            return View();
        }
    }
}
