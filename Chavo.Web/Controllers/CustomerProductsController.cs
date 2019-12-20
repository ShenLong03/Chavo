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

namespace Chavo.Web.Controllers
{
    public class CustomerProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: CustomerProducts
        public async Task<ActionResult> Index()
        {
            var customerProducts = db.CustomerProducts.Include(c => c.Customer).Include(c => c.Product);
            return View(await customerProducts.ToListAsync());
        }

        // GET: CustomerProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // GET: CustomerProducts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "ID");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: CustomerProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerProductId,CustomerId,ProductId,Date,Active")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.CustomerProducts.Add(customerProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "ID", customerProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        // GET: CustomerProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "ID", customerProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        // POST: CustomerProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerProductId,CustomerId,ProductId,Date,Active")] CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "ID", customerProduct.CustomerId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        // GET: CustomerProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            if (customerProduct == null)
            {
                return HttpNotFound();
            }
            return View(customerProduct);
        }

        // POST: CustomerProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerProduct customerProduct = await db.CustomerProducts.FindAsync(id);
            db.CustomerProducts.Remove(customerProduct);
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
