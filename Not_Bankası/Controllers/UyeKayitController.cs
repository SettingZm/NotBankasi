using Not_Bankası.Controllers.UyeKontrolleri;
using Not_Bankası.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [AllowAnonymous]
    public class UyeKayitController : Controller
    {
        Not_BankasiEntities NotBankasiDB = new Not_BankasiEntities();
        // GET: UyeKayit
        public ActionResult UyeKayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UyeKayit(UyeKontrol KayitOlanUye)
        {
            var varmi = NotBankasiDB.UyeBilgileris.FirstOrDefault(x => x.EMail == KayitOlanUye.Email);
            if (varmi == null)
            {
                UyeBilgileri uye = new UyeBilgileri();

                if (KayitOlanUye.Sifre == KayitOlanUye.SifreOnay)
                {
                    uye.İsim = KayitOlanUye.Isim;
                    uye.Soyisim = KayitOlanUye.Soyisim;
                    uye.Sifre = KayitOlanUye.Sifre;
                    uye.EMail = KayitOlanUye.Email;
                    uye.Resim = "user.jpg";
                    uye.Universite_Id = 1;
                    uye.Bolum_Id = 1;
                    NotBankasiDB.UyeBilgileris.Add(uye);
                    NotBankasiDB.SaveChanges();
                    return RedirectToAction("UyeGiris", "UyeGiris");
                }
                else
                    ViewBag.mesaj += "Şifreler eşleşmiyor";
            }
            else
            {
                ViewBag.mesaj += "Sistemde böyle bir kullanıcı var";
            }
            return View();
        }


    }
}