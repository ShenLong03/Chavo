namespace Chavo.Web.Controllers
{
    using Data;
    using Data.Entity;
    using Helpers;
    using Models;
    using System.Data.Entity;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class CategoriesController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = new CategoryViewModel();
            AutoMapper.Mapper.Map(category, model);
            return View(model);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Categories";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                var category = new Category();
                AutoMapper.Mapper.Map(model, category);
                db.Categories.Add(category);
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
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var model = new CategoryViewModel();
            AutoMapper.Mapper.Map(category, model);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = model.Picture;
                var folder = "~/Content/Categories";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                var category = new Category();
                AutoMapper.Mapper.Map(model, category);
                db.Entry(category).State = EntityState.Modified;
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
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            db.Categories.Remove(category);
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
