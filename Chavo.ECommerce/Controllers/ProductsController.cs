namespace Chavo.ECommerce.Controllers
{
    using Chavo.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
                   
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.SubCategory.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s => s.CategoryId == product.SubCategory.CategoryId), "SubCategoryId", "Name", product.SubCategoryId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", product.WeightId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature", product.CurrencyId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature", product.UnitLengthId);
            return View(product);
        }

        [Authorize]
        public async Task<ActionResult> BuyProduct(int id)
        {
            var product = await db.Products.FindAsync(id);

            if (product==null)
            {
                return RedirectToAction("Index", "Home");
            }
            var customer = db.Customers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
            db.CustomerProducts.Add(new CustomerProduct
            {
                Active = true,
                CustomerId = customer.CustomerId,
                Date = DateTime.Now,
                ProductId=product.ProductId
            });
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "PrivateZone"); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}