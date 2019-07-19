using Not_Bankası.Models.iletisim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [AllowAnonymous]
    public class iletisimController : Controller
    {
        // GET: iletisim
        //@* bursaı kullanıcı ve user ile ortak sayfa*@
        public ActionResult iletisim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult iletisim(iletisimSinifi model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad: " + model.isim);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Konu);
                body.AppendLine("Mesaj: " + model.Mesaj);
                Mail.MailSender(body.ToString());
                ViewBag.Success = true;
            }
            return View();
        }
    }
}