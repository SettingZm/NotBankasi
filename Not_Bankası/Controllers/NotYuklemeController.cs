using Not_Bankası.Controllers.GirisKontrolleri;
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
    public class NotYuklemeController : Controller
    {
        Not_BankasiEntities db = new Not_BankasiEntities();
        // GET: NotYukleme
        public ActionResult NotYukleme()
        {
            List<Universite> unis = db.Universites.ToList();

            return View(unis);
        }
        //ee napcam kaydetmiyor ve böyle kalıyor break pin yapalım girmiyor bile
        [HttpPost]
        public ActionResult NotYukleme(HttpPostedFileBase YukelenenNot, string DersAdi, string NotAdi, string Aciklama, string OgretimUyesi, string comboUni, string comboBolum, string Yil, string Donem)
        {
            if (DersAdi == "" || NotAdi == "" || Aciklama == "" || comboUni == "" || comboBolum == "" || Yil == "" || Donem == "" || OgretimUyesi == "")
            {
                string error = "Boş Bırakılmış Alanlar Mevcut";
                ViewBag.Error = error;
                return RedirectToAction("NotYukleme");
            }
            if (YukelenenNot != null)
            {
                if (YukelenenNot != null && YukelenenNot.ContentLength > 0)
                {

                    var directory = Server.MapPath("~/Notlar");


                    if (YukelenenNot.ContentLength > 10485760)
                    {
                        string error = "Yüklemek İstediğiniz Dosya 10mb'dan Fazla olamaz";
                        ViewBag.Error = error;
                        return RedirectToAction("NotYukleme");
                    }

                    var supportedTypes = new[] { "pdf" };

                    var fileExt = Path.GetExtension(YukelenenNot.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        string error = "Yanlızca PDF türünde dosyalar Yükleyebilirsiniz.";
                        ViewBag.Error = error;
                        return RedirectToAction("NotYukleme");
                    }

                    var fileName = Path.GetFileName(YukelenenNot.FileName);
                    

                    int KullaniciID = Convert.ToInt32(Session["KullaniciID"]);
                    int bolumID = db.Universite_Bolum.Where(x => x.BolumAdı == comboBolum).Select(x => x.Bolum_Id).FirstOrDefault();

                    NotBilgileri not = new NotBilgileri();
                    not.NotAdı = DersAdi;
                    not.DersOgretimUyesi = OgretimUyesi;
                    not.Universite_Id = Convert.ToInt32(comboUni);
                    not.Yıl = Yil;
                    not.Not = fileName;
                    not.DersAdı = DersAdi;
                    not.Donem = Donem;
                    not.Bolum_Id = bolumID;
                    not.Uye_Id = KullaniciID;

                    not.NotunAçıklaması = Aciklama;
                    db.NotBilgileris.Add(not);

                    db.SaveChanges();
                    YukelenenNot.SaveAs(Path.Combine(directory, fileName));
                    return RedirectToAction("AnaSayfa", "AnaSayfa");
                }

            }
            return RedirectToAction("AnaSayfa", "AnaSayfa");
        }

        [HttpPost]
        public JsonResult BolumSecme(int id)
        {
            var UniversiteBolumler = (from c in db.Universite_Bolum
                                      where c.Universite_Id == id
                                      select c.BolumAdı).ToList();

            return Json(UniversiteBolumler);
        }
    }
}