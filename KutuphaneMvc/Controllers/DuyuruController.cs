﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLDUYURU.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURU t)
        {
            db.TBLDUYURU.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURU.Find(id);
            db.TBLDUYURU.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruBilgiler(TBLDUYURU p)
        {
            var duyuru = db.TBLDUYURU.Find(p.ID);
            return View("DuyuruBilgiler", duyuru);
        }
        public ActionResult DuyuruGuncelle(TBLDUYURU t)
        {
            var duyuru = db.TBLDUYURU.Find(t.ID);
            duyuru.KATEGORI = t.KATEGORI;
            duyuru.ICERIK = t.ICERIK;
            duyuru.TARIH = t.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}