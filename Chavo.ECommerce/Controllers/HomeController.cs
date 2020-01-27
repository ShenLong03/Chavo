namespace Chavo.ECommerce.Controllers
{
    using Common;
    using Data;
    using Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        DataContextLocal db = new DataContextLocal();

        public async Task<ActionResult> Index(int? orden, int[] categories, int[] subCategories, double? min, double? max, bool acquiereProduct = false)
        {
            var view = new HomeViewModel();
            if (db.GeneralConfigurations.Count() > 0)
            {
                view.GeneralConfiguration = db.GeneralConfigurations.FirstOrDefault();

                if (!string.IsNullOrEmpty(db.GeneralConfigurations.FirstOrDefault().VideoBanner))
                {
                    view.VideoBanner = db.GeneralConfigurations.FirstOrDefault().VideoBanner;
                }
            }

            IQueryable<Product> productsSelect = db.Products.Where(p=>p.State==State.Disponible && p.Active);

            if (acquiereProduct)
            {
                productsSelect = productsSelect.Where(p => p.DisplayAcquiereProduct);
            }

            if (categories != null && subCategories != null)
            {
                if (categories[0] != 0 && subCategories[0] != 0)
                {
                    productsSelect = productsSelect.Where(p => categories.Contains(p.SubCategory.CategoryId) || subCategories.Contains(p.SubCategoryId));
                }
                else if (categories[0] != 0 && subCategories[0] == 0)
                {
                    productsSelect = productsSelect.Where(p => categories.Contains(p.SubCategory.CategoryId));
                }
                else if (categories[0] == 0 && subCategories[0] != 0)
                {
                    productsSelect = productsSelect.Where(p => subCategories.Contains(p.SubCategoryId));
                }
            }
            if (min != null)
            {
                productsSelect = productsSelect.Where(p => p.PriceAmount >= min);
            }
            if (max != null)
            {
                productsSelect = productsSelect.Where(p => p.PriceAmount <= max);
            }
            switch (orden)
            {
                case 0:
                    productsSelect = productsSelect.OrderBy(p => p.Name);
                    break;
                case 1:
                    productsSelect = productsSelect.OrderByDescending(p => p.Name);
                    break;
                case 2:
                    productsSelect = productsSelect.OrderBy(p => p.PriceAmount);
                    break;
                case 3:
                    productsSelect = productsSelect.OrderByDescending(p => p.PriceAmount);
                    break;
                default:
                    break;
            }
            view.Products = await productsSelect.ToListAsync();
            view.Categories = await db.Categories.Include(c => c.SubCategories).ToListAsync();
            view.DisplayClothes = false;
            if (User.Identity.IsAuthenticated)
            {
                var customer = db.Customers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
                if (customer!=null)
                {
                    view.DisplayClothes = customer.DisplayClothes;
                }
            }

            return View(view);
        }

        public ActionResult Footer()
        {
             if (db.GeneralConfigurations.Count()>0)
             {
                 return View(db.GeneralConfigurations.FirstOrDefault());
             }
             else
             {
                 return View(new GeneralConfiguration());
             }            
        }

        public ActionResult BuildSite()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult TransacctionNumber()
        {
            if (db.GeneralConfigurations.Count() > 0)
            {
                return View(db.GeneralConfigurations.FirstOrDefault());
            }
            else
            {
                return View(new GeneralConfiguration());
            }
        }

        public ActionResult Contact()
        {
            if (db.GeneralConfigurations.Count() > 0)
            {
                return View(db.GeneralConfigurations.FirstOrDefault());
            }
            else
            {
                return View(new GeneralConfiguration());
            }
        }
    }
}