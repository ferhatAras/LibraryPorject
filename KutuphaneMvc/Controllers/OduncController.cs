using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [Authorize(Roles ="A")]
        public ActionResult Index()
        {
            var degerler = db.TBLHARAKET.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD +" " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from y in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from z in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = z.PERSONEL,
                                               Value = z.ID.ToString()
                                           }).ToList();
            ViewBag.Dgr1 = deger1;
            ViewBag.Dgr2 = deger2;
            ViewBag.Dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHARAKET p)
        {
            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(y => y.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(z => z.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = d1;
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;
            db.TBLHARAKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Odunciade(TBLHARAKET p)
        {
            var odn = db.TBLHARAKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);
        }
        public ActionResult OduncGuncelle(TBLHARAKET p)
        {
            var hrk = db.TBLHARAKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult IADETARIH()
        {
            // İade işlemi gerçekleştiğinde tarih ataması yapabilirsiniz
            DateTime iadeTarihi = DateTime.Now;

            // Diğer işlemler ve dönüş
            // ...

            return View();
        }
    }
}