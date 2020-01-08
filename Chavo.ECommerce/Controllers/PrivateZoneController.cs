namespace Chavo.ECommerce.Controllers
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class PrivateZoneController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        public ActionResult Index()
        {
            return View(db.Customers.FirstOrDefault());
        }

        public ActionResult AssignedInvestor()
        {
            var customer = db.Customers.FirstOrDefault();
            return View(customer);
        }
    }
}