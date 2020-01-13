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
    public class SubCategoriesController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.SubCategories.Include(s=>s.Category).ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            var model = new SubCategoryViewModel();
            AutoMapper.Mapper.Map(subCategory, model);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            return View(model);
        }

        [HandleError]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View(new SubCategoryViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/SubCategories";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                var subCategory = new SubCategory();
                AutoMapper.Mapper.Map(model, subCategory);
                db.SubCategories.Add(subCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            return View(model);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            var model = new SubCategoryViewModel();
            AutoMapper.Mapper.Map(subCategory, model);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            return View(model);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = model.Picture;
                var folder = "~/Content/SubCategories";

                if (model.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(model.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                model.Picture = pic;
                var subCategory = new SubCategory();
                AutoMapper.Mapper.Map(model, subCategory);
                db.Entry(subCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", model.CategoryId);
            return View(model);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = await db.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            db.SubCategories.Remove(subCategory);
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
