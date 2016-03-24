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
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Roles
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            if (role.Name == "" || string.IsNullOrWhiteSpace(role.Name))
            {
                ModelState.AddModelError("", "Invalid Role Name");
                return View(role);
            }

            try
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Role with name already exists");
                return View(role);
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            if (role.Name == "" || string.IsNullOrWhiteSpace(role.Name))
            {
                ModelState.AddModelError("", "Invalid Role Name");
                return View(role);
            }

            IdentityRole oldRole = db.Roles.Find(role.Id);
            if (oldRole.Name == "Admin")
            {
                ModelState.AddModelError("", "Cannot rename the role 'Admin'. This is a protected role to make sure the website doesn't become inaccessible.");
                return View(role);
            }

            try
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Role with name already exists");
                return View(role);
            }
            
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityRole role = db.Roles.Find(id);

            if (role.Name == "Admin")
            {
                ModelState.AddModelError("", "Cannot delete the role 'Admin'. This is a protected role to make sure the website doesn't become inaccessible.");
                return View(role);
            }
            else {
                db.Roles.Remove(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
