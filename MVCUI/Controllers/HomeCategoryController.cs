using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public class HomeCategoryController : Controller
    {
        CategoryRepository cRep;
        public HomeCategoryController()
        {
            cRep = new CategoryRepository();
        }
        // GET: Category
        public ActionResult Index()
        {
            return PartialView("_CategoryList", cRep.GetActives());
        }
    }
}