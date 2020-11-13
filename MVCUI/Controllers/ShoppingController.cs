using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using COMMON.Tools;
using MODEL.Entities;
using MVCUI.Models;
using MVCUI.Models.ShoppingTools;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository oRep;
        ProductRepository pRep;
        CategoryRepository cRep;
        OrderDetailRepository odRep;
        PARepository paRep;
        public ShoppingController()
        {
            oRep = new OrderRepository();
            pRep = new ProductRepository();
            cRep = new CategoryRepository();
            odRep = new OrderDetailRepository();
            paRep = new PARepository();
        }
        // GET: Product
        public ActionResult Index(int? page, int? categoryID)
        {
            PAVM pavm = new PAVM()
            {
                PagedProducts = categoryID == null ? pRep.GetActives().ToPagedList(page ?? 1, 9) : pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
                Categories = cRep.GetActives()
            };
            if (categoryID != null) TempData["catID"] = categoryID;
            return View(pavm);
        }

        public ActionResult ProductDetail(int? id)
        {
            PAVM pavm = new PAVM()
            {
                Product = pRep.Default(x => x.ID == id),
                ProductAttributes = paRep.Where(x=> x.ProductID == id),
            };
            return View(pavm);
        }


        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            Product addToProduct = pRep.Find(id);
            CartItem ci = new CartItem
            {
                ID = addToProduct.ID,
                Name = addToProduct.ProductName,
                Price = addToProduct.UnitPrice,
                ImagePath = addToProduct.ImagePath
            };
            c.AddToCart(ci);
            Session["scart"] = c;
            return RedirectToAction("Index");
        }

        public ActionResult CartPage()
        {
            if (Session["scart"]!=null)
            {
                Cart c = Session["scart"] as Cart;
                return View(c);
            }
            TempData["emptyCart"] = "Sepetinizde Ürün Bulunmamaktadır";
            return RedirectToAction("Index","Shopping");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.DeleteToCart(id);
                if (c.MyCart.Count==0)
                {
                    Session.Remove("scart");
                    TempData["emptyCart"] = "Sepetinizde Ürün Bulunmamaktadır";
                    return RedirectToAction("Index","Shopping");
                }
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("Index","Shopping");
        }

        public ActionResult ConfirmOrder()
        {
            AppUser user;
            if (Session["member"] != null)
            {
                user = Session["member"] as AppUser;
            }
            else
            {
                TempData["anonim"] = "Kullanıcı Üye Değil";
            }
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmOrder(OrderVM ovm)
        {
            bool result;
            Cart cart = Session["scart"] as Cart;
            ovm.Order.TotalPrice = ovm.PaymentVM.ShoppingPrice = cart.TotalPrice.Value;

            //

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59328/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", ovm.PaymentVM);

                HttpResponseMessage sonuc;
                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka Bağlatıyı Reddetti";
                    return RedirectToAction("Index");
                }

                if (sonuc.IsSuccessStatusCode)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            if (result)
            {
                if (Session["member"] != null)
                {
                    AppUser user = Session["member"] as AppUser;
                    ovm.Order.AppUserID = user.ID;
                    ovm.Order.UserName = user.UserName;
                }
                else
                {
                    ovm.Order.AppUserID = null;
                    ovm.Order.UserName = TempData["anonim"].ToString();
                }

                oRep.Add(ovm.Order);

                foreach (CartItem item in cart.MyCart)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderID = ovm.Order.ID;
                    od.ProductID = item.ID;
                    od.TotalPrice = item.SubTotal;
                    od.Quantity = item.Amount;
                    odRep.Add(od);

                    Product stockAmount = pRep.Find(item.ID);
                    stockAmount.UnitsInStock -= item.Amount;
                    pRep.Update(stockAmount);
                }

                TempData["pay"] = "Siparişiniz Bize Ulaşmıştır Teşekkür Ederiz";
                MailSender.Send(ovm.Order.Email, body: $"Siparişiniz Başarıyla Alındı {ovm.Order.TotalPrice}");
                return RedirectToAction("Index");
            }
            else
            {
                TempData["fail"] = "Ödeme ile ilgili bir sorun oluştu. Lütfen bankanızla iletişime geçiniz.";
                return RedirectToAction("Index");
            }
        }
    }
}