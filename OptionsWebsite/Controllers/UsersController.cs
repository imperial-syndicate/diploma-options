using DiplomaDataModel.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OptionsWebsite.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            List<IdentityRole> roles = new List<IdentityRole>();
            foreach (var role in user.Roles)
            {
                IdentityRole r = db.Roles.Find(role.RoleId);
                roles.Add(r);
            }
            ViewBag.roles = roles;
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityUser user)
        {
            try
            {
                IdentityUser oldUser = db.Users.Find(user.Id);
                oldUser.LockoutEnabled = user.LockoutEnabled;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Whoops, looks like an error occurred");
                return View(user);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            List<IdentityRole> roles = new List<IdentityRole>();
            foreach (var role in user.Roles)
            {
                IdentityRole r = db.Roles.Find(role.RoleId);
                roles.Add(r);
            }
            ViewBag.roles = roles;
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            bool isValid = true;
            ApplicationUser user = db.Users.Find(id);

            if (User.Identity.Name == user.UserName)
            {
                isValid = false;
                ModelState.AddModelError("", "You cannot delete the account you're logged into");
            }
            if (user.UserName == "A00111111")
            {
                isValid = false;
                ModelState.AddModelError("", "You cannot delete the user A00111111");
            }

            if (!isValid)
            {
                List<IdentityRole> roles = new List<IdentityRole>();
                foreach (var role in user.Roles)
                {
                    IdentityRole r = db.Roles.Find(role.RoleId);
                    roles.Add(r);
                }
                ViewBag.roles = roles;
                return View(user);
            }

            db.Users.Remove(user);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
