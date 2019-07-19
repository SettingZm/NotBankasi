using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    [AllowAnonymous]
    public class HakkindaController : Controller
    {
        // GET: Hakkinda
        //@* bursaı kullanıcı ve user ile ortak sayfa*@
        public ActionResult Hakkinda()
        {
            return View();
        }
    }
}