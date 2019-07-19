using Not_Bankası.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Not_Bankası.Controllers
{
    public class UyeGirisController : Controller
    {
        Not_BankasiEntities NotBankasiDB = new Not_BankasiEntities();
        // GET: UyeGiris
        [AllowAnonymous]
        public ActionResult UyeGiris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return RedirectToAction("UyeProfilDuzenle", "UyeProfilDuzenle");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UyeGiris(UyeBilgileri GirisYapanUye, string benihatirla)
        {
            if (ModelState.IsValid)
            {
                var Kontrol = NotBankasiDB.UyeBilgileris.FirstOrDefault(x => x.EMail == GirisYapanUye.EMail && x.Sifre == GirisYapanUye.Sifre);
                if (Kontrol != null)
                {
                    //if (benihatirla == "true")
                    //FormsAuthentication.SetAuthCookie(GirisYapanUye.EMail, true);

                    if (benihatirla == "true")
                    {
                        var cookie = FormsAuthentication.GetAuthCookie(GirisYapanUye.EMail, true);
                        cookie.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(GirisYapanUye.EMail, false);
                    }

                    UyeBilgileri UyeBilgileri = NotBankasiDB.UyeBilgileris.Where(x => x.EMail == GirisYapanUye.EMail).FirstOrDefault();

                    //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UyeBilgileri.Uye_Id.ToString(), DateTime.Now, DateTime.Now.AddMinutes(Session.Timeout), true, UyeBilgileri.EMail);
                    //HttpCookie giris = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    //Response.Cookies.Add(giris);

                    //UyeBilgileri.Bolum_Id = 1;
                    //UyeBilgileri.Universite_Id = 1;
                    //UyeBilgileri.Not_Id = 1;
                    //UyeBilgileri.Resim = "~/img/user.png";
                    Session.Add("KullaniciIsmi", UyeBilgileri.İsim);
                    Session.Add("KullaniciResmi", UyeBilgileri.Resim);
                    Session.Add("KullaniciID", UyeBilgileri.Uye_Id);

                    return RedirectToAction("AnaSayfa", "AnaSayfa");
                }
                else
                {
                    ModelState.AddModelError("", "Mail veya şifre hatalı!");
                }
            }
            return View();
        }

        public ActionResult UyeCikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UyeGiris", "UyeGiris");
        }
    }
}