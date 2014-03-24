using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Protal.Models;

namespace Protal.Areas.User.Controllers
{
    public class UserController : Controller
    {
         blogdbEntities db=new blogdbEntities();
        //
        // GET: /User/User/

        public ActionResult Index()
        {
            
            return View(db.users_info);
        }

    }
}
