using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    public class Hata404Controller : Controller
    {
        // GET: Hata404
        //@* bursaı kullanıcı ve user ile ortak sayfa*@
        public ActionResult Hata()
        {
            return View();
        }
    }
}