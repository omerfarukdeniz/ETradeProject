using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using COMMON.Tools;
using MODEL.Entities;
using MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository apRep;
        public HomeController()
        {
            apRep = new AppUserRepository();
        }
        public ActionResult Index()
        {
            return PartialView("_UserLogin",Session["member"]);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser item)
        {
            AppUser user = apRep.Default(x => x.UserName == item.UserName);
            string decrypted = DantexCrypt.DeCrypt(user.Password);
            if (item.Password==decrypted && user!= null)
            {
                if (user.Role == UserRole.Admin)
                {
                    if (!user.IsActive)
                    {
                        ActiveControl();
                    }
                    else
                    {
                        Session["admin"] = user;
                        return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                    }
                }
                else if (user.Role == UserRole.Member)
                {
                    if (!user.IsActive)
                    {
                        ActiveControl();
                    }
                    else
                    {
                        Session["member"] = user;
                        return RedirectToAction("Index", "Shopping");
                    }
                    
                }
            }
            else
            {
                ViewBag.UserNull = "Kullanıcı Bulunamadı";
            }
            return View(user);
        }

        public ActionResult ActiveControl()
        {
            ViewBag.IsActive = "Lütfen Hesabınızı aktif hale getiriniz. Mailinizi kontrol ediniz.";
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            if (Session["member"] != null)
            {
                Session.Remove("member");
            }
            else if (Session["member"]!= null && Session["scart"] != null)
            {
                Session.Remove("member");
                Session.Remove("scart");
            }
            return RedirectToAction("Index","Shopping");
        }
    }
}