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
    public class InvestorProductsController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            var investorProducts = db.InvestorProducts.Include(i => i.Investor).Include(i => i.Product);
            return View(await investorProducts.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestorProduct investorProduct = await db.InvestorProducts.FindAsync(id);
            if (investorProduct == null)
            {
                return HttpNotFound();
            }
            return View(investorProduct);
        }

        [HandleError]
        public ActionResult Create()
        {
            ViewBag.CustomerInvestorId = new SelectList(db.CustomerInvestors, "CustomerInvestorId", "CustomerInvestorId");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvestorProduct investorProduct)
        {
            if (ModelState.IsValid)
            {
                db.InvestorProducts.Add(investorProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerInvestorId = new SelectList(db.CustomerInvestors, "CustomerInvestorId", "CustomerInvestorId", investorProduct.CustomerInvestorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestorProduct investorProduct = await db.InvestorProducts.FindAsync(id);
            if (investorProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerInvestorId = new SelectList(db.CustomerInvestors, "CustomerInvestorId", "CustomerInvestorId", investorProduct.CustomerInvestorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InvestorProduct investorProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investorProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerInvestorId = new SelectList(db.CustomerInvestors, "CustomerInvestorId", "CustomerInvestorId", investorProduct.CustomerInvestorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvestorProduct investorProduct = await db.InvestorProducts.FindAsync(id);
            if (investorProduct == null)
            {
                return HttpNotFound();
            }
   
            db.InvestorProducts.Remove(investorProduct);
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
