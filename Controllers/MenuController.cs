using dov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dov.Controllers
{
    public class MenuController : Controller
    {

        QLHHDataContext _db = new QLHHDataContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            var v = from t in _db.ThucDons
                    select t;
            return PartialView(v.ToList());
        }
    }
}