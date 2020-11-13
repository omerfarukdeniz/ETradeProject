using MVCUI.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["admin"] != null)
            {
                Session.Remove("admin");
            }
            return RedirectToAction("Index","AdminHome");
        }
    }
}