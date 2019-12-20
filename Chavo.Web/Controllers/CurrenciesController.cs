namespace Chavo.Web.Controllers
{
    using Data;
    using Data.Entity;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CurrenciesController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.Currencies.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Currencies.Add(currency);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currency).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }

            db.Currencies.Remove(currency);
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
