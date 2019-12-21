using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chavo.Common;
using Chavo.ECommerce.Data;

namespace Chavo.ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Currency).Include(p => p.SubCategory).Include(p => p.UnitLength).Include(p => p.WeightType);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
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
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name");
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Name");
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,Name,Code,Serie,Model,Age,Weight,WeightId,Width,Height,Picture,Description,CostAmount,PriceAmount,Discount,Date,Video,SubCategoryId,CurrencyId,UnitLengthId,Active")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", product.CurrencyId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name", product.SubCategoryId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Name", product.UnitLengthId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", product.WeightId);
            return View(product);
        }

        // GET: Products/Edit/5
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
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", product.CurrencyId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name", product.SubCategoryId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Name", product.UnitLengthId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", product.WeightId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,Name,Code,Serie,Model,Age,Weight,WeightId,Width,Height,Picture,Description,CostAmount,PriceAmount,Discount,Date,Video,SubCategoryId,CurrencyId,UnitLengthId,Active")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "CurrencyId", "Name", product.CurrencyId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "Name", product.SubCategoryId);
            ViewBag.UnitLengthId = new SelectList(db.UnitLengths, "UnitLengthId", "Name", product.UnitLengthId);
            ViewBag.WeightId = new SelectList(db.Weights, "WeightId", "Name", product.WeightId);
            return View(product);
        }

        // GET: Products/Delete/5
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
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
