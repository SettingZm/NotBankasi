using Not_Bankası.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [AllowAnonymous]
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        //@* bursaı kullanıcı ve user ile ortak sayfa yani üye olanda üye olmayanda buraya erişecek fakat üye olan kendi teması ile üye olmayan kendi temsaı ile*@
        Not_BankasiEntities db = new Not_BankasiEntities();

        public ActionResult AnaSayfa()
        {
            List<Universite> UniversitelerListesi = db.Universites.ToList();


            return View(UniversitelerListesi);
        }

        [HttpPost]
        public JsonResult BolumSecme(int id)
        {
            var UniversiteBolumler = (from c in db.Universite_Bolum
                                      where c.Universite_Id == id
                                      select c.BolumAdı).ToList();

            return Json(UniversiteBolumler);
        }

        [HttpPost]
        public ActionResult TumNotlar(string aranacak, string comboUni)
        {
            int uid = 0;
            if (comboUni == "Üniversite")
            {
                uid = 0;
            }
            else
            {
                uid = Convert.ToInt32(comboUni);
            }

            List<NotBilgileri> notlar;
            var uniID = db.Universites.Where(x => x.Universite_Id == uid).Select(x => x.Universite_Id).FirstOrDefault();
            if (uniID == 0)
            {
                if (aranacak == "")
                {
                    notlar = db.NotBilgileris.ToList();
                }
                else
                {
                    notlar = db.NotBilgileris.Where(n => n.NotAdı.ToLower().Contains(aranacak)).ToList();
                }
            }
            else
            {
                if (aranacak == "")
                {
                    notlar = db.NotBilgileris.Where(x => x.Universite_Id == uniID).ToList();
                }
                else
                {
                    notlar = db.NotBilgileris.Where(n => n.NotAdı.ToLower().Contains(aranacak)).ToList();
                    notlar = notlar.Where(n => n.Universite_Id == uniID).ToList();
                }
            }
            return View("TumNotlar", notlar);
        }
        [HttpGet]
        public ActionResult DownloadFile(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Notlar/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + file);
            string fileName = file;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}