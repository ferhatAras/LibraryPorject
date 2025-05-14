using KutuphaneMvc.Models.Entities;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace KutuphaneMvc.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        // Veritabanı bağlantısı
        private readonly DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }

            db.TBLUYELER.Add(p);

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        System.Diagnostics.Debug.WriteLine($"Entity: {validationErrors.Entry.Entity.GetType().Name}, Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                // Hata durumunda formu tekrar gösterir
                return View("Kayit");
            }

            // Başarılı kayıt durumunda yönlendirme yapar
            return RedirectToAction("GirisYap", "Login");
        }

    }
}
