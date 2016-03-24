using DiplomaDataModel.Identity;
using Microsoft.AspNet.Identity;
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

            // Find the User
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Find their roles
            ViewBag.roles = getUserRoles(user);

            // Get a list of all available roles
            List<String> userRoles = user.Roles.Select(s => s.RoleId).ToList();
            ViewBag.rolesList = getAllRoles(userRoles);

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityUser user, FormCollection collection)
        {
            bool isValid = true;
            try
            {
                IdentityUser oldUser = db.Users.Find(user.Id);

                // Update the users locked status
                if (oldUser.UserName == "A00111111" && user.LockoutEnabled == true)
                {
                    ModelState.AddModelError("", "Cannot lock the user 'A00111111'.");
                    isValid = false;
                }
                else {
                    oldUser.LockoutEnabled = user.LockoutEnabled;
                    db.SaveChanges();
                }

                // Role Management
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);

                // Remove any roles from the user
                if (collection["userRoles[]"] != null)
                {
                    string[] collect = collection["userRoles[]"].Split(',');
                    foreach (var role in collect)
                    {
                        if (oldUser.UserName == "A00111111" && role == "Admin")
                        {
                            ModelState.AddModelError("", "Cannot remove Admin role from this user");
                            isValid = false;
                        }
                        else {
                            userManager.RemoveFromRole(oldUser.Id, role);
                        }
                    }
                }

                // The admin wants to add a role to the user
                if (collection["Roles"] != "")
                {
                    userManager.AddToRole(user.Id, collection["Roles"]);
                }

                // Find their roles
                ViewBag.roles = getUserRoles(oldUser);

                // Get a list of all available roles
                List<String> userRoles = oldUser.Roles.Select(s => s.RoleId).ToList();
                ViewBag.rolesList = getAllRoles(userRoles);

                if (isValid)
                {
                    ViewBag.successMessage = "User has been successfully updated";
                }
                return View(oldUser);
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

        private List<IdentityRole> getUserRoles(IdentityUser user)
        {
            List<IdentityRole> roles = new List<IdentityRole>();
            foreach (var role in user.Roles)
            {
                IdentityRole r = db.Roles.Find(role.RoleId);
                roles.Add(r);
            }

            return roles;
        }

        private List<SelectListItem> getAllRoles(List<String> userRoles)
        {
            List<SelectListItem> allRoles = new List<SelectListItem>();
            allRoles.Add(new SelectListItem
            {
                Value = "",
                Text = "--NONE--"
            });
            foreach (var listItem in db.Roles.Where(s => !userRoles.Contains(s.Id))
                          .Select(s => new SelectListItem
                          {
                              Value = s.Name,
                              Text = s.Name
                          }).ToList())
            {
                allRoles.Add(listItem);
            }

            return allRoles;
        }
    }
}
