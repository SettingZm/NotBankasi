using Not_Bankası.Controllers.GirisKontrolleri;
using Not_Bankası.Controllers.UyeKontrolleri;
using Not_Bankası.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [_SessionController]
    public class UyeProfilDuzenleController : Controller
    {
        Not_BankasiEntities NotBankasiDB = new Not_BankasiEntities();
        // GET: UyeProfilDuzele
        public ActionResult UyeProfilDuzenle()
        {
            
            List<Universite> unis = NotBankasiDB.Universites.ToList();
            ViewBag.Unis = unis;
            int kullanciID = Convert.ToInt32(Session["KullaniciID"]);
            UyeBilgileri uye = NotBankasiDB.UyeBilgileris.Where(x => x.Uye_Id == kullanciID).FirstOrDefault();
            return View(uye);
        }

        [HttpPost]
        public JsonResult BolumSecme(int id)
        {
            var UniversiteBolumler = (from c in NotBankasiDB.Universite_Bolum
                                      where c.Universite_Id == id
                                      select c.BolumAdı).ToList();

            return Json(UniversiteBolumler);
        }

        [HttpPost]
        public ActionResult UyeProfilDuzenle(HttpPostedFileBase ProfilResmi, UyeKontrol GuncellenenUye, string comboUni, string comboBolum)
        //Allah affettis abi
        {
            ViewBag.Mesaj = "";
            UyeBilgileri uye = new UyeBilgileri();
            int kullanciID = Convert.ToInt32(Session["KullaniciID"]);
            int bolumID = NotBankasiDB.Universite_Bolum.Where(x => x.BolumAdı == comboBolum).Select(x => x.Bolum_Id).FirstOrDefault();
            uye = NotBankasiDB.UyeBilgileris.FirstOrDefault(x => x.Uye_Id == kullanciID);

            if (GuncellenenUye.Sifre == GuncellenenUye.SifreOnay)
            {
                if (ProfilResmi != null)
                {
                    string dosyaAdi = Path.GetFileName(ProfilResmi.FileName);
                    string path = Path.Combine(Server.MapPath("~/img"), dosyaAdi);
                    ProfilResmi.SaveAs(Server.MapPath($"~/img/{dosyaAdi}"));
                    uye.Resim = dosyaAdi;
                }
                uye.EMail = GuncellenenUye.Email;
                uye.İsim = GuncellenenUye.Isim;
                uye.Soyisim = GuncellenenUye.Soyisim;
                uye.Sifre = GuncellenenUye.Sifre;
                uye.Universite_Id = Convert.ToInt32(comboUni);
                uye.Bolum_Id = bolumID;
                NotBankasiDB.SaveChanges();
                //ViewBag.Isim = uye.İsim;
                //ViewBag.Soyad = uye.Soyisim;
                //ViewBag.Sifre = uye.Sifre;
                //ViewBag.Universite = uye.Universite.UniversiteAdı;
                //ViewBag.Mail = uye.EMail;
                ViewBag.Mesaj += "Kayıt İşlemi Tamamlandı";
                //ViewBag.Resim = uye.Resim;
                Session["KullaniciResmi"] = uye.Resim;
                List<Universite> unis = NotBankasiDB.Universites.ToList();
                ViewBag.Unis = unis;
                return View(uye);
            }
            else
                ViewBag.mesaj += "Şifreler eşleşmiyor";

            
            return View();
        }
    }


}