using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Not_Bankası.Controllers
{
    public class TumNotlarController : Controller
    {
        // GET: TumNotlar
        //@* bursaı kullanıcı ve user ile ortak sayfa*@
        public ActionResult TumNotlar()
        {
            return View();
        }
    }
}