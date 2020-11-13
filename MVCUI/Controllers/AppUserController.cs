using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using MVCUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        AppUserRepository auRep;
        AppUserDetailRepository audRep;
        public AppUserController()
        {
            auRep = new AppUserRepository();
            audRep = new AppUserDetailRepository();
        }

        
        public ActionResult Index(int? id)
        {
            if (Session["member"]!= null)
            {
                AppUserVM appUserVM = new AppUserVM()
                {
                    AppUser = auRep.Default(x => x.ID == id),
                    AppUserDetail = audRep.Default(x => x.AppUser.ID == id)
                };
                return View(appUserVM);
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult AppUserUpdate(int id)
        {
            if (Session["member"]!= null)
            {
                AppUserVM appUserVM = new AppUserVM()
                {
                    AppUser = auRep.Default(x => x.ID == id),
                    AppUserDetail = audRep.Default(x=> x.ID==id),
                };
                return View(appUserVM);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AppUserUpdate(AppUserVM item)
        {
            if (ModelState.IsValid)
            {
                //auRep.Update(item.AppUser);
                audRep.Update(item.AppUserDetail);
                ViewBag.Status = 1;
                return View();
            }
            else
            {
                ViewBag.Status = 2;
                return View();
            }
        }
    }
}