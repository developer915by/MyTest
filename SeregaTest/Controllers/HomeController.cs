using Microsoft.AspNet.Identity;
using SeregaTest.Data;
using SeregaTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SeregaTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        public ActionResult Subscribe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();

            var user = db.Users.Find(userId);

            @event.Users.Add(user);
            db.Entry(@event).State = EntityState.Modified;
            db.SaveChanges();

            return View("ManageSubscriptions",false);
        }

        public ActionResult Unsubscribe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            @event.Users.Remove(user);
            db.Entry(@event).State = EntityState.Modified;
            db.SaveChanges();

            return View("ManageSubscriptions", false);
        }

        public ActionResult Subscriptions()
        {
            var userId = User.Identity.GetUserId();
            var events = db.Events.Where(item => item.Users.Select(u => u.Id).Contains(userId)).ToList();
            return View(events);
        }
    }
}