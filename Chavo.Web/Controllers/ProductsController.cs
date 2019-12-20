using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chavo.Web.Data;
using Chavo.Web.Data.Entity;
using Chavo.Web.Models;
using Chavo.Web.Helpers;

namespace Chavo.Web.Controllers
{
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.SubCategory);
            return View(await products.ToListAsync());
        }

        [HandleError]
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
            var model = new ProductViewModel();
            AutoMapper.Mapper.Map(product, model);

            model.CategoryId = model.SubCategory.CategoryId;
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s => s.CategoryId == model.CategoryId), "SubCategoryId", "Name", model.SubCategoryId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", model.WeightId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature", model.CurrencyId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature", model.UnitLengthId);
            return View(model);
        }

        [HandleError]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s=>s.CategoryId==db.Categories.FirstOrDefault().CategoryId), "SubCategoryId", "Name");
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Nomenclature");
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature");
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature");

            return View(new ProductViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Products";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Picture = pic;              

                var product = new Product();
                AutoMapper.Mapper.Map(view, product);
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", view.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s => s.CategoryId == view.CategoryId), "SubCategoryId", "Name", view.SubCategoryId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", view.WeightId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature", view.CurrencyId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature", view.UnitLengthId);
            return View(view);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
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

            var model = new ProductViewModel();
            AutoMapper.Mapper.Map(product,model);

            model.CategoryId = model.SubCategory.CategoryId;
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s => s.CategoryId == model.CategoryId), "SubCategoryId", "Name", model.SubCategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name", product.SubCategoryId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", model.WeightId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature", model.CurrencyId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature", model.UnitLengthId);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Picture;
                var folder = "~/Content/Products";

                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Picture = pic;

                var product = new Product();
                AutoMapper.Mapper.Map(view, product);
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", view.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories.Where(s => s.CategoryId == view.CategoryId), "SubCategoryId", "Name", view.SubCategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name", view.SubCategoryId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", view.WeightId);
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Nomenclature", view.CurrencyId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Nomenclature", view.UnitLengthId);

            return View(view);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
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

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetSubCategories(int id)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(await db.SubCategories.Where(s => s.CategoryId == id).ToListAsync(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new List<SubCategory>(), JsonRequestBehavior.AllowGet);
            }
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
