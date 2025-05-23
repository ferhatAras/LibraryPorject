﻿using KutuphaneMvc.Models.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KutuphaneMvc.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Panel
        [HttpGet]

        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TBLUYELER.FirstOrDefault(z => z.MAIL == uyemail);
            var degerler= db.TBLDUYURU.ToList();

            var d1=db.TBLUYELER.Where(x=>x.MAIL==uyemail).Select(y=>y.AD).FirstOrDefault();              /*Üye ad ve soyad getirir*/
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;

            var d3 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;

            var d4 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.d4 = d4;

            var d5 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.OKUL).FirstOrDefault();
            ViewBag.d5 = d5;

            var d6 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;

            var d7 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeid = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.ID).FirstOrDefault();
            var d8 = db.TBLHARAKET.Where(x => x.UYE == uyeid).Count();      
            ViewBag.d8 = d8 ;

            var d9 =db.TBLMESAJLAR.Where(x=>x.ALICI==uyemail).Count();
            ViewBag.d9 = d9;

            var d10 = db.TBLDUYURU.Count();
            ViewBag.d10 = d10;

            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);

            if (uye != null)
            {
                uye.SIFRE = p.SIFRE;
                uye.KULLANICIADI = p.KULLANICIADI;
                uye.AD = p.AD;
                uye.SOYAD = p.SOYAD; 
                uye.OKUL = p.OKUL;
                uye.KULLANICIADI = p.KULLANICIADI;
                uye.TELEFON = p.TELEFON;
                uye.FOTOGRAF = p.FOTOGRAF;
                db.SaveChanges();
            }

            return RedirectToAction("Index"); 
        }
        public ActionResult Kitaplarim()
        {
            var kullanıcı = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanıcı.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLHARAKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURU.ToList();
            return View(duyurulistesi);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [Authorize]
        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici).Select(y => y.ID).FirstOrDefault();
            var uyebul = db.TBLUYELER.Find(id);
            return PartialView("Partial2",uyebul);
        }

    }
}