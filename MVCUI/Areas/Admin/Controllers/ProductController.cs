using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using COMMON.Tools;
using MODEL.Entities;
using MVCUI.Areas.Admin.Models;
using MVCUI.AuthenticationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class ProductController : Controller
    {
        ProductRepository pRep;
        CategoryRepository cRep;
        public ProductController()
        {
            pRep = new ProductRepository();
            cRep = new CategoryRepository();
        }
        public ActionResult Index(int? id)
        {
            if (id==null)
            {
                return View(pRep.GetAll());
            }
            return View(pRep.Where(x=> x.CategoryID==id));
        }

        public ActionResult GetActiveProduct(int? id)
        {
            if (id == null)
            {
                return View(pRep.GetActives());
            }
            return View(pRep.Where(x => x.CategoryID == id));
        }

        public ActionResult GetPassiveProduct(int? id)
        {
            if (id == null)
            {
                return View(pRep.GetPassives());
            }
            return View(pRep.Where(x => x.CategoryID == id));
        }

        public ActionResult AddProduct()
        {
            ProductCategoryVM pcvm = new ProductCategoryVM();
            pcvm.Product = new Product();
            pcvm.Categories = cRep.GetAll();
            return View(pcvm);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductCategoryVM vmModel, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                vmModel.Product.ImagePath = ImageUploader.UploadImage("~/Pictures/", image);
                pRep.Add(vmModel.Product);
                ViewBag.Status = 1;
                return RedirectToAction("AddProduct");
            }
            else
            {
                ViewBag.Status = 2;
                return View();
            }
            
        }

        public ActionResult UpdateProduct(int id)
        {
            ProductCategoryVM pcvm = new ProductCategoryVM();
            pcvm.Categories = cRep.GetAll();
            pcvm.Product = pRep.Find(id);
            return View(pcvm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                pRep.Update(model.Product);
                ViewBag.Status = 1;
                return RedirectToAction("");
            }
            else
            {
                ViewBag.Status = 2;
                return View();
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            Product p = new Product();
            p.CategoryID = null;
            pRep.Delete(pRep.Find(id));
            return RedirectToAction("GetActiveProduct");
        }
    }
}