namespace Chavo.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Data;
    using Data.Entity;
    using Chavo.Web.Models;
    using Chavo.Web.Helpers;

    [Authorize]
    public class GeneralConfigurationsController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            if (db.GeneralConfigurations.Count()>0)
            {
                return RedirectToAction("Edit", new { id = db.GeneralConfigurations.FirstOrDefault().GeneralConfigurationId });
            }

            return RedirectToAction("Create");
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfiguration generalConfiguration = await db.GeneralConfigurations.FindAsync(id);
            if (generalConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(generalConfiguration);
        }

        [HandleError]
        public ActionResult Create()
        {
            return View(new GeneralConfigurationViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(GeneralConfigurationViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/GeneralConfigurations";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Logo = pic;

                pic = string.Empty;                
                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Picture = pic;

                var generalConfiguration = new GeneralConfiguration();
                AutoMapper.Mapper.Map(view, generalConfiguration);

                db.GeneralConfigurations.Add(generalConfiguration);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        [HandleError]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfiguration generalConfiguration = await db.GeneralConfigurations.FindAsync(id);
            if (generalConfiguration == null)
            {
                return HttpNotFound();
            }
            var view = new GeneralConfigurationViewModel();
            AutoMapper.Mapper.Map(generalConfiguration, view);
            return View(view);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(GeneralConfigurationViewModel view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Logo;
                var folder = "~/Content/GeneralConfigurations";

                if (view.LogoFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.LogoFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Logo = pic;

                pic = string.Empty;
                if (view.PictureFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.PictureFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                view.Picture = pic;

                var generalConfiguration = new GeneralConfiguration();
                AutoMapper.Mapper.Map(view, generalConfiguration);

                db.Entry(generalConfiguration).State=EntityState.Modified;
                await db.SaveChangesAsync();
                ViewBag.Message = "Save successfull";
                return View(view);
            }

            return View(view);
        }

        [HandleError]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfiguration generalConfiguration = await db.GeneralConfigurations.FindAsync(id);
            if (generalConfiguration == null)
            {
                return HttpNotFound();
            }

            db.GeneralConfigurations.Remove(generalConfiguration);
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
