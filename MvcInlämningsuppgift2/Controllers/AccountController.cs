using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInlämningsuppgift2.Models;
using System.Web.Security;

namespace MvcInlämningsuppgift2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Registerera()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registerera(Kund nyKund)
        {
            using (TomasosEntities db = new TomasosEntities())
            {

                db.Kunds.Add(nyKund);
                db.SaveChanges();
                ViewBag.Message = "Du är registererad.";
                return View();


            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Kund kund)
        {
            using (TomasosEntities db = new TomasosEntities())
            {
                Kund userLoggedIn = db.Kunds.SingleOrDefault(u => u.AnvandarNamn == kund.AnvandarNamn && u.Losenord == kund.Losenord);

                if (userLoggedIn == null)
                {
                    ViewBag.ErrorLogin = "Du har angivit fel uppgifter";
                    return View();
                }

                else
                {
                    FormsAuthentication.SetAuthCookie(kund.AnvandarNamn, false);
                    Session["ID"] = userLoggedIn.KundID;
                    return RedirectToAction("Meny", "Home");
                }

            }

        }

        public ActionResult Uppdatera()
        {
            string username = HttpContext.User.Identity.Name;
            using (TomasosEntities db = new TomasosEntities ())
            {
                Kund nyKund = db.Kunds.SingleOrDefault(k => k.AnvandarNamn == username);
                return View(nyKund);
            }
            
           
        }

        [HttpPost]
        public ActionResult Uppdatera(Kund inloggadKund)
        {
            
            using (TomasosEntities db = new TomasosEntities())
            {
                Kund nyKund = db.Kunds.SingleOrDefault(k => k.KundID == inloggadKund.KundID);
                nyKund.AnvandarNamn = inloggadKund.AnvandarNamn;
                nyKund.Namn = inloggadKund.Namn;
                nyKund.Gatuadress = inloggadKund.Gatuadress;
                nyKund.Postnr = inloggadKund.Postnr;
                nyKund.Postort = inloggadKund.Postort;
                nyKund.Email = inloggadKund.Email;
                nyKund.Telefon = inloggadKund.Telefon;
                nyKund.Losenord = inloggadKund.Losenord;

                db.SaveChanges();

                return View();
            }

        }



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Start", "Home");
        }
    }
}