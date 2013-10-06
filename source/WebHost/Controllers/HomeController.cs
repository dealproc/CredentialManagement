using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.Controllers
{
    public class HomeController : Controller
    {
        IDataContext database;
        public HomeController(IDataContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            database = db;

        }
        public ActionResult Index()
        {
            Seed();
            return View();
        }

        private void Seed()
        {
            if (database.Permissions.All().Count() == 0)
            {
                for (var i = 0; i < 100; i++)
                {
                    database.Permissions.Insert(new Permission()
                    {
                        Code = "Permission-" + (1 + i).ToString(),
                        Name = "Permission " + (1 + i).ToString()
                    });
                    database.Commit();
                }
            }

            if (database.Roles.All().Count() == 0)
            {
                database.Roles.Insert(new Role()
                {
                    HasAllPermissions = true,
                    Name = "System Administrator"
                });
                database.Commit();
            }

            if (database.Groups.All().Count() == 0)
            {
                database.Groups.Insert(new Group()
                {
                    AutoAssign = true,
                    Name = "All Users",
                    GroupKey = "ALL_USERS",
                    Description = "contains all users, cannot be deleted"
                });
                database.Commit();
            }
        }
    }
}
