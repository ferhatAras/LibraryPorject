﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBLKITAP.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFile dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");

        }
        public ActionResult LinqKart()
        {
            var deger1 = db.TBLKITAP.Count();
            var deger2 = db.TBLUYELER.Count();
            var deger3 = db.TBLCEZALAR.Sum(x => x.PARA);
            var deger4 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            var deger5 = db.TBLKATEGORI.Count();
            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();
            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            var deger11 = db.TBLILETISIM.Count();           
            var deger6 = db.TBLHARAKET.GroupBy(x => x.TBLUYELER.AD).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            var deger10 = db.TBLPERSONEL.GroupBy(x => x.PERSONEL).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault().Key;
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr6 = deger6;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11 = deger11;
            return View();
        }

    }
}