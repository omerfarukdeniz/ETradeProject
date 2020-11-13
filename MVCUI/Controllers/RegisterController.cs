using BLL.DesignPatterns.RepositoryPattern.ConcRep;
using COMMON.Tools;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        AppUserRepository apRep;
        AppUserDetailRepository apdRep;
        public RegisterController()
        {
            apRep = new AppUserRepository();
            apdRep = new AppUserDetailRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AppUser appUser, AppUserDetail appUserDetail)
        {
            appUser.Password = DantexCrypt.Crypt(appUser.Password);
            if (apRep.Any(x=> x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Bu kullanıcı ismi daha önce alınmış!";
                return View();
            }
            else if (apRep.Any(x=> x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Bu Email Zaten Kayıtlı";
                return View();
            }
            string sendMail = "Tebrikler, hesabınız oluşturuldu. Hesabınızı Aktive etmek için http://localhost:54696/Register/Activation/" + appUser.ActivationCode + " linkine tıklayabilirsiniz.";
            MailSender.Send(appUser.Email, body: sendMail, subject: "Hesap Aktivasyon!");
            apRep.Add(appUser);

            if (!string.IsNullOrEmpty(appUserDetail.FirstName) || !string.IsNullOrEmpty(appUserDetail.LastName) || !string.IsNullOrEmpty(appUserDetail.Adress1) || !string.IsNullOrEmpty(appUserDetail.Adress2))
            {
                appUserDetail.ID = appUser.ID;
                apdRep.Add(appUserDetail);
            }
            return View("RegisterOk");
        }


        public ActionResult Activation(Guid id)
        {
            if (apRep.Any(x=> x.ActivationCode==id))
            {
                AppUser activation = apRep.Default(x => x.ActivationCode == id);
                activation.IsActive = true;

                apRep.Update(activation);
                TempData["HesapAktif"] = "Hesabınız Aktif Hale Getirildi.";
                return RedirectToAction("Login","Home");
            }
            else
            {
                TempData["HesapAktif"] = "Aktif Edilecek Hesap Bulunamadı !";
                return RedirectToAction("Index","Register");
            }
        }

        public ActionResult RegisterOk()
        {
            return View();
        }
    }
}