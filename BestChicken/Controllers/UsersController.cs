using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BestChicken.Models;
using BestChicken.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BestChicken.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            //por el db tambien se obtiene user y rol
            var users = db.Users.ToList();
            var rol = db.Roles.ToList();

            var usermag = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var lis = usermag.Users.ToList();

            List<UserView> userViews = new List<UserView>();
            foreach (var user in lis)
            {
                UserView u = new UserView
                {
                    Name = user.UserName,
                    UserId = user.Id,
                    Email = user.Email
                };
                userViews.Add(u);
            }
            return View(userViews);
        }

        public ActionResult Roles(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roles = roleManager.Roles.ToList();

            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == id);
            var rolesview = new List<RoleView>();

            foreach (var r in user.Roles)
            {
                var role = roles.Find(c => c.Id == r.RoleId);
                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };
                rolesview.Add(roleView);
            }

            var userView = new UserView
            {
                Name = user.UserName,
                UserId = user.Id,
                Email = user.Email,
                Roles = rolesview
            };

            return View(userView);
        }

        [HttpPost]
        public ActionResult Roles()
        {

            return View();
        }
        [HttpGet]
        public ActionResult AddRole(string id)
        {
            Session["id"] = id;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(us => us.Id == id);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();

            List<RoleView> roleViews = new List<RoleView>();
            foreach (var role in roles)
            {
                var roleView = new RoleView();
                roleView.Name = role.Name;
                roleView.RoleId = role.Id;
                roleViews.Add(roleView);
            }

            roleViews.Add(new RoleView { RoleId = "0", Name = "[Select a Client]" });
            roleViews = roleViews.OrderBy(costumer => costumer.Name).ToList();
            ViewBag.RoleId = new SelectList(roleViews, "RoleId", "Name");


            return View();
        }

        [HttpPost]
        public ActionResult AddRole()
        {
            string id = Session["id"].ToString();
            string downText = Request["RoleId"];
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (downText == "0")
            {
                var roles = roleManager.Roles.ToList();

                List<RoleView> roleViews = new List<RoleView>();
                foreach (var role in roles)
                {
                    var roleView = new RoleView();
                    roleView.Name = role.Name;
                    roleView.RoleId = role.Id;
                    roleViews.Add(roleView);
                }
                roleViews.Add(new RoleView { RoleId = "0", Name = "[Select a Role]" });
                roleViews = roleViews.OrderBy(costumer => costumer.Name).ToList();
                ViewBag.RoleId = new SelectList(roleViews, "RoleId", "Name");
                ViewBag.Mess = "Please, Select a Role";
                return View();
            }

            var R = roleManager.FindById(downText);

            if (!userManager.IsInRole(id, R.Name))
            {
                userManager.AddToRole(id, R.Name);

                var roles = roleManager.Roles.ToList();

                List<RoleView> roleViews = new List<RoleView>();
                foreach (var role in roles)
                {
                    var roleView = new RoleView();
                    roleView.Name = role.Name;
                    roleView.RoleId = role.Id;
                    roleViews.Add(roleView);
                }
                roleViews.Add(new RoleView { RoleId = "0", Name = "[Select a Client]" });
                roleViews = roleViews.OrderBy(costumer => costumer.Name).ToList();
                ViewBag.RoleId = new SelectList(roleViews, "RoleId", "Name");
                ViewBag.Mess = "The role " + R.Name + " is add correctly";
                return View();
            }
            else
            {
                var roles = roleManager.Roles.ToList();

                List<RoleView> roleViews = new List<RoleView>();
                foreach (var role in roles)
                {
                    var roleView = new RoleView();
                    roleView.Name = role.Name;
                    roleView.RoleId = role.Id;
                    roleViews.Add(roleView);
                }
                roleViews.Add(new RoleView { RoleId = "0", Name = "[Select a Client]" });
                roleViews = roleViews.OrderBy(costumer => costumer.Name).ToList();
                ViewBag.RoleId = new SelectList(roleViews, "RoleId", "Name");
                ViewBag.Mess = "The role " + R.Name + " exist";
                return View();
            }
        }

        public ActionResult Delete (string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindById(id);
            UserView userView = new UserView
            {
                Name = user.UserName,
                Email = user.Email,
                UserId = user.Id
            };

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(userView);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string id)
        {
            string sid = id.ToString();
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindById(sid);
            usermanager.Delete(user);
            var users = usermanager.Users.ToList();
            List<UserView> userViews = new List<UserView>();
            UserView userView = new UserView();
            for (int i = 0; i < users.Count; i++)
            {
                userView = new UserView
                {
                    Name = users[i].UserName,
                    Email = users[i].Email,
                    UserId = users[i].Id
                };
                userViews.Add(userView);
            }
            return View("Index", userViews);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}