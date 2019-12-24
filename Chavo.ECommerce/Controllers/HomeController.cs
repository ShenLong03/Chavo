using Chavo.ECommerce.Data;
using Chavo.ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Chavo.ECommerce.Controllers
{
    public class HomeController : Controller
    {
        DataContextLocal db = new DataContextLocal();

        public async Task<ActionResult> Index()
        {
            var view = new HomeViewModel();
            if (db.GeneralConfigurations.Count()>0)
            {
                if (!string.IsNullOrEmpty(db.GeneralConfigurations.FirstOrDefault().VideoBanner))
                {
                    view.VideoBanner = db.GeneralConfigurations.FirstOrDefault().VideoBanner;
                }
            }
            view.Products = await db.Products.ToListAsync();
            return View(view);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}