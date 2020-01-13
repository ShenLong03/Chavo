namespace Chavo.ECommerce.Controllers
{
    using Chavo.ECommerce.Models;
    using Data;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class PrivateZoneController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        public ActionResult Index()
        {
            var customer = db.Customers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
            var view = new CustomerViewModel();
            AutoMapper.Mapper.Map(customer, view);
            var generalConfiguration = db.GeneralConfigurations.FirstOrDefault();
            if (generalConfiguration!=null)
            {
                view.GeneralConfigurations = generalConfiguration;
            }
            return View(view);
        }

        public ActionResult AssignedInvestor()
        {
            var customer = db.Customers.FirstOrDefault();
            return View(customer);
        }
    }
}