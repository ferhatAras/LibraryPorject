using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class islemController : Controller
    {
        // GET: islem
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLHARAKET.Where(x => x.ISLEMDURUM == true).ToList();
            return View(degerler);
        }
    }
}