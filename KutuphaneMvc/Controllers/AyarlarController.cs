using System;
using System.Linq;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class AyarlarController : Controller
    {
        // Veritabanı bağlantısı
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            var kullanicilar = db.tbladmın.ToList();
            return View(kullanicilar);
        }

        public ActionResult Index2()
        {
            var kullanicilar = db.tbladmın.ToList();
            return View(kullanicilar);
        }

        // Yeni Admin ekleme - GET
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        // Yeni Admin ekleme - POST
        [HttpPost]
        public ActionResult YeniAdmin(tbladmın t)
        {
            if (ModelState.IsValid)
            {
                db.tbladmın.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(t);
        }

        // Admin Silme
        public ActionResult AdminSil(int id)
        {
            var admin = db.tbladmın.Find(id);
            if (admin != null)
            {
                db.tbladmın.Remove(admin);
                db.SaveChanges();
            }
            return RedirectToAction("Index2");
        }

        // Admin Güncelleme - GET
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = db.tbladmın.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View("AdminGuncelle", admin); // Görünüm adının doğru olduğundan emin olun
        }

        // Admin Güncelleme - POST
        [HttpPost]
        public ActionResult AdminGuncelle(tbladmın p)
        {
            if (ModelState.IsValid)
            {
                var admin = db.tbladmın.Find(p.ID);
                if (admin != null)
                {
                    admin.Kullanici = p.Kullanici;
                    admin.Sifre = p.Sifre;
                    admin.Yetki = p.Yetki;
                    db.SaveChanges();
                }
                return RedirectToAction("Index2");
            }
            return View(p);
        }
    }
}
