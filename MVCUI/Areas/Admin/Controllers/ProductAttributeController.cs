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
    public class ProductAttributeController : Controller
    {
        PARepository paRep;
        EARepository eaRep;
        ProductRepository pRep;
        public ProductAttributeController()
        {
            paRep = new PARepository();
            eaRep = new EARepository();
            pRep = new ProductRepository();
        }
        // GET: Admin/ProductAttribute
        public ActionResult Index(int id)
        {
            return View(pRep.Find(id));
        }

        [HttpPost]
        public ActionResult ProductAttributeListValue(int id, FormCollection collection)
        {
            List<ProductAttribute> currentData = paRep.Where(x => x.ProductID == id);
            int indexer = 0;
            foreach (ProductAttribute element in currentData)
            {
                element.Value = collection.GetValues("valueName")[indexer];
                indexer++;
                paRep.Update(element);
            }
            return RedirectToAction("ProductDetail", new { id = id });
        }

        public ActionResult ProductDetail(int id)
        {
            return View(paRep.Where(x => x.ProductID == id));
        }

        public ActionResult ProductAttributeAdd(int id)
        {
            ViewBag.AttributeList = eaRep.GetAll();
            return View(pRep.Find(id));
        }

        [HttpPost]
        public ActionResult ProductAttributeAdd(Product item, FormCollection collection)
        {
            foreach (string element in collection.GetValues("checkbox"))
            {
                int id = Convert.ToInt32(element);
                ProductAttribute pa = new ProductAttribute();
                pa.ProductID = item.ID;
                pa.AttributeID = id;
                paRep.Add(pa);
            }
            return RedirectToAction("Index", new { id = item.ID });
        }
    }
}