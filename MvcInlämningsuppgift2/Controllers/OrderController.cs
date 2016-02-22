using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcInlämningsuppgift2.Models;


namespace MvcInlämningsuppgift2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult AddToCart(int id)
        {
            List<Matratt> selMatratts = null;
            TomasosEntities db = new TomasosEntities();
            
            Matratt matratt = db.Matratts.SingleOrDefault(p => p.MatrattID == id);
        
            if (Session["Varukorg"] == null)
            {
                selMatratts = new List<Matratt>();
                selMatratts.Add(matratt);
            }
            else
            {                
                selMatratts = (List<Matratt>)Session["Varukorg"];
                selMatratts.Add(matratt);
            }

            Session["Varukorg"] = selMatratts;
            ViewBag.TotalSumman = selMatratts.Sum(t => t.Pris);
            TempData["summan"] = selMatratts.Sum(t => t.Pris);
            return PartialView("_OrderPartialView", selMatratts);

        }

        public ActionResult bestallning (/*int id*/)
        {
            using (TomasosEntities db = new TomasosEntities())
            {
                //Bestallning nyBestallning = db.Bestallnings.SingleOrDefault(b => b.BestallningID == id);
                Bestallning nyBestallning = new Bestallning();
                nyBestallning.BestallningDatum = DateTime.Now;
                nyBestallning.Totalbelopp = (int)TempData["summan"];
                nyBestallning.Levererad = true;
                nyBestallning.KundID = (int)Session["ID"];

                db.Bestallnings.Add(nyBestallning);
                db.SaveChanges();

                int id = nyBestallning.BestallningID;

                foreach (var item in (List<Matratt>)Session["Varukorg"])
                {
                    BestallningMatratt matratt = new BestallningMatratt();

                    matratt.MatrattID = item.MatrattID;
                    matratt.BestallningID = id;
                    matratt.Antal = 1;

                    db.BestallningMatratts.Add(matratt);
                    db.SaveChanges();

                }

                ViewBag.Message = "Din ordern har skickats!";
                Session["Varukorg"] = null;
               return PartialView("_OrderPartialView", nyBestallning);
            }
        }
    }
}