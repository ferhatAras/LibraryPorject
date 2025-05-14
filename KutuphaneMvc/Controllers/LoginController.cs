using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneMvc.Models.Entities;
using System.Web.Security;


namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    //[Authorize]
    public class LoginController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        // GET: Login
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            // Kullanıcının email ve şifresini doğrula
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);

            if (bilgiler != null)
            {
                // Giriş başarılı
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL;

                return RedirectToAction("Index", "Panel");
            }
            else
            {
                // Hatalı giriş mesajı
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                return View("GirisYap", p);
            }
        }
    }
}