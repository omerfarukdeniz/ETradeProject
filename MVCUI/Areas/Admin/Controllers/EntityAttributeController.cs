using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using MODEL.Entities;
using MVCUI.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class EntityAttributeController : Controller
    {
        EARepository eaRep;
        public EntityAttributeController()
        {
            eaRep = new EARepository();
        }
        // GET: Admin/EntityAttribute
        public ActionResult Index()
        {
            return View(eaRep.GetActives());
        }

        public ActionResult GetAllEA()
        {
            return View(eaRep.GetAll());
        }
        public ActionResult AddEA()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEA(EntityAttribute model)
        {
            if (ModelState.IsValid)
            {
                eaRep.Add(model);
                ViewBag.Status = 1;
            }
            else
            {
                ViewBag.Status = 2;
                return View();
            }
            return View();
        }

        public JsonResult DeleteEA(int id)
        {
            eaRep.Delete(eaRep.Find(id));
            return Json("");
        }

        public ActionResult UpdateEA(int id)
        {
            return View(eaRep.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateEA(EntityAttribute model)
        {
            if (ModelState.IsValid)
            {
                eaRep.Update(model);
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