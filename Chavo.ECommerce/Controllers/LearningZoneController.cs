namespace Chavo.ECommerce.Controllers
{
    using Chavo.ECommerce.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class LearningZoneController : Controller
    {
        DataContextLocal db = new DataContextLocal();

        public ActionResult Index()
        {
            var generalConfiguration = db.GeneralConfigurations.FirstOrDefault();
           
            return View(generalConfiguration);
        }
    }
}