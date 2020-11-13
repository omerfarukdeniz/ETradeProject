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
    public class CategoryController : Controller
    {
        CategoryRepository cRep;
        public CategoryController()
        {
            cRep = new CategoryRepository();
        }
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(cRep.GetActives());
        }

        public ActionResult GetDeletedCategories()
        {
            return View(cRep.GetPassives());
        }
        public ActionResult GetUpdatedCategories()
        {
            return View(cRep.GetModifieds());
        }
        public ActionResult GetAllCategories()
        {
            return View(cRep.GetAll());
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                cRep.Add(model);
                ViewBag.Status = 1;
            }
            else
            {
                ViewBag.Status = 2;
                return View();
            }
            return View();
        }

        public JsonResult DeleteCategory(int id)
        {
            cRep.Delete(cRep.Find(id));
            RedirectToAction("Index");
            return Json("");
        }

        public ActionResult UpdateCategory(int id)
        {
            return View(cRep.Find(id));
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category item)
        {
            if (ModelState.IsValid)
            {
                cRep.Update(item);
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