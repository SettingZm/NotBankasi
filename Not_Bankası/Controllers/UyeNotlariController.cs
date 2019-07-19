using Not_Bankası.Controllers.GirisKontrolleri;
using Not_Bankası.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [_SessionController]
    public class UyeNotlariController : Controller
    {
        Not_BankasiEntities db = new Not_BankasiEntities();
        // GET: UyeNotlari
        public ActionResult UyeNotlari()
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            List<NotBilgileri> kullaniNotlari = db.NotBilgileris.Where(x => x.Uye_Id == kullaniciID).ToList();
            return View(kullaniNotlari);
        }
        public ActionResult NotSil(int NotId)
        {
            NotBilgileri nb = new NotBilgileri();
            nb = db.NotBilgileris.Where(x => x.Not_Id == NotId).FirstOrDefault();
            db.NotBilgileris.Remove(nb);
            db.SaveChanges();
            return RedirectToAction("UyeNotlari");
        }
    }
}