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