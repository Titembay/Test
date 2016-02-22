using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInlämningsuppgift2.Models;

namespace MvcInlämningsuppgift2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Start()
        {
            return View();
        }
        [Authorize]
        public ActionResult Meny()
        {
            using (TomasosEntities db = new TomasosEntities())
            {
                List<MatrattTyp> menyLista = db.MatrattTyps.Include("Matratts").Select(m => m).ToList();
                return View(menyLista);

            }
            
        }

        public ActionResult Kontakt()
        {
            return View();
        }
    }
}