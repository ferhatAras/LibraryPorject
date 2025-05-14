using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class MesajController : Controller
    {
        // GET: Mesaj
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [Authorize]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).ToList();
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult Giden()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gidenMesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).ToList();
            return View(gidenMesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["Mail"].ToString();
            t.GONDEREN = uyemail;
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden", "Mesaj");
        }
       public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gelensayisi=db.TBLMESAJLAR.Where(x=>x.ALICI== uyemail).Count();
            ViewBag.d1=gelensayisi;
            var gidensayisi = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d2 = gidensayisi;
            return PartialView();
        }

       
    }
}
