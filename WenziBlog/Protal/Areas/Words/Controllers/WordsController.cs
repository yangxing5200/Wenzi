using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Logging;
using Protal.Models;

namespace Protal.Areas.Words.Controllers
{
    public class WordsController : Controller
    {
        
        private ILog log = LogManager.GetLogger("web");
        //
        // GET: /Words/Words/

        public ActionResult Index()
        {
            using (blogdbEntities db = new blogdbEntities())
            {
                log.Info("WordsController.Index" + DateTime.Now.ToString());
            return View(db.wz_word.ToList());
            }
            
        }
        public ActionResult Add()
        {
            log.Info("WordsController.Add(GET)" + DateTime.Now.ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Add(wz_word model)
        {
            using (blogdbEntities db = new blogdbEntities())
            {
                log.Info("WordsController.Add(pOST)" + DateTime.Now.ToString());
                model.CreateDate = DateTime.Now;
                model.Creator = "system";
                model.IsDelete = 0;

                db.wz_word.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Words");
            }
        }

        [HttpGet]
        public ActionResult Modify(int id)
        {
            using (blogdbEntities db = new blogdbEntities())
            {
                wz_word model = (from d in db.wz_word where d.Id == id && d.IsDelete == 0 select d).FirstOrDefault();

                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Modify(wz_word model)
        {
            using (blogdbEntities db = new blogdbEntities())
            {
                model.ModifyDate = DateTime.Now;
                var entityEntry = db.Entry(model);
                //entityEntry.Entity.Id = model.Id;
                entityEntry.State = EntityState.Unchanged;
                entityEntry.Property(x => x.Content).IsModified = true;
                entityEntry.Property(x => x.Authority).IsModified = true;
                entityEntry.Property(x => x.ModifyDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Words");
            }
        }

        public ActionResult Del(int id)
        {
            using (blogdbEntities db = new blogdbEntities())
            {
                var model = new wz_word {Id = id};
                db.wz_word.Attach(model);
                db.wz_word.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Words");
            }
        }


    }
}
