namespace Chavo.ECommerce.Controllers
{
    using Common;
    using Data;
    using Helpers;
    using Models;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CustomersController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return RedirectToAction("Details", new { userName = User.Identity.Name });
        }

        [HandleError]
        public async Task<ActionResult> Details(string userName)
        {
            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.Where(c => c.UserName == userName).FirstOrDefaultAsync();
            if (customer == null)
            {
                return HttpNotFound();
            }
            var view = new CustomerViewModel();
            AutoMapper.Mapper.Map(customer, view);
            return View(view);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var model = new CustomerViewModel();
            AutoMapper.Mapper.Map(customer, model);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = model.Picture;
                var folder = "~/Content/Customers";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                model.UserName = model.Email;
                if (!string.IsNullOrEmpty(model.BirthDateString))
                    model.BirthDate = DateTime.ParseExact(model.BirthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var customer = new Customer();
                AutoMapper.Mapper.Map(model, customer);

                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            db.Customers.Remove(customer);
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
