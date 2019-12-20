namespace Chavo.Web.Controllers
{
    using Helpers;
    using Models;
    using Data;
    using Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class FunctionariesController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.Functionaries.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Functionary functionary = await db.Functionaries.FindAsync(id);
            if (functionary == null)
            {
                return HttpNotFound();
            }
            var model = new FunctionaryViewModel();
            AutoMapper.Mapper.Map(functionary, model);
            return View(model);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View(new FunctionaryViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FunctionaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Functionaries";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                model.UserName = model.Email;
                if (!string.IsNullOrEmpty(model.BirthDateString))
                    model.BirthDate = DateTime.ParseExact(model.BirthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var functionary = new Functionary();
                AutoMapper.Mapper.Map(model, functionary);

                db.Functionaries.Add(functionary);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Functionary functionary = await db.Functionaries.FindAsync(id);
            if (functionary == null)
            {
                return HttpNotFound();
            }
            var model = new FunctionaryViewModel();
            AutoMapper.Mapper.Map(functionary, model);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FunctionaryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = model.Picture;
                var folder = "~/Content/Functionaries";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                model.UserName = model.Email;
                if (!string.IsNullOrEmpty(model.BirthDateString))
                    model.BirthDate = DateTime.ParseExact(model.BirthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                var functionary = new Functionary();
                AutoMapper.Mapper.Map(model, functionary);

                db.Entry(functionary).State = EntityState.Modified;
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
            Functionary functionary = await db.Functionaries.FindAsync(id);
            if (functionary == null)
            {
                return HttpNotFound();
            }

            db.Functionaries.Remove(functionary);
            await db.SaveChangesAsync();

            DeleteUser(functionary.UserName);

            return RedirectToAction("Index");
        }

        public void DeleteUser(string userName)
        {
            try
            {
                ApplicationDbContext ap = new ApplicationDbContext();
                string vUserID;


                if (ap.Users.Where(u => u.UserName == userName).ToList().Count() > 0)
                {
                    vUserID = ap.Users.Where(u => u.UserName == userName).First().Id;

                    //creamos la conexion a la tabla
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ap));
                    //traemos una lista de la tabla
                    var user = userManager.Users.ToList().Find(u => u.Id == vUserID);
                    ap.Users.Remove(user);
                    ap.SaveChanges();
                }

                ap.Dispose();
            }
            catch (Exception ex)
            {


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
