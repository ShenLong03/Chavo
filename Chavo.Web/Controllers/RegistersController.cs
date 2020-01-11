namespace Chavo.Web.Controllers
{
    using Chavo.Web.Helpers;
    using Data;
    using Data.Entity;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class RegistersController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index(bool aproved = false)
        {
            return View(await db.Registers.Where(r => r.Approved == aproved).ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = await db.Registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Register register)
        {
            if (ModelState.IsValid)
            {
                db.Registers.Add(register);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(register);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = await db.Registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (register.Approved)
                {
                    if (!string.IsNullOrEmpty(register.Email))
                    {
                        await MailHelper.SendMail(register.Email, "Cuenta Romanico Aprobada", "Su cuenta de Romanico.com fue aprobada. Su contraseña temporal es " + register.Password + ".");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(register);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = await db.Registers.FindAsync(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            db.Registers.Remove(register);
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
