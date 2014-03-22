using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Protal.Models;

namespace Protal.Areas.Words.Controllers
{
    public class WordsController : Controller
    {
        blogdbEntities1 db=new blogdbEntities1();
        //
        // GET: /Words/Words/

        public ActionResult Index()
        {
            return View(db.wz_word.ToList());
        }

    }
}
