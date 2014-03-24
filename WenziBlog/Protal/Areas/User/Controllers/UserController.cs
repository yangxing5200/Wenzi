using System;
using System.Collections.Generic;
using System.Data;
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
            
            return View(db.users_info.ToList());
        }
        public ActionResult Del(int id)
        {
            var user = new users_info {Id = id};
            db.users_info.Attach(user);
            db.users_info.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        public ActionResult Save(users_info model)
        {
            if (model.Id == 0)
            {
                db.users_info.Add(model);
                db.SaveChanges();
            }
            else
            {
                var dbEntry = db.Entry(model);
                dbEntry.State = EntityState.Unchanged;
                dbEntry.Property(x => x.UserName).IsModified = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "User");
        }
    }
}
