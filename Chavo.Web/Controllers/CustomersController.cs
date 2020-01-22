namespace Chavo.Web.Controllers
{
    using Chavo.Web.Data.Entity;
    using Data;
    using Data.Entity;
    using Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class CustomersController : Controller
    {
        private DataContext db = new DataContext();

        [HandleError]
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        [HandleError]
        public async Task<ActionResult> Details(int? id)
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
        public ActionResult Create()
        {
            return View(new CustomerViewModel());
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
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

                db.Customers.Add(customer);
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

            DeleteUser(customer.UserName);

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

        #region CustomerProduct
        public async Task<ActionResult> DetailsCustomerProduct(int? id)
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

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        [HandleError]
        public async Task<ActionResult> CreateCustomerProduct(int? id, string message)
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

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");

            if (!string.IsNullOrEmpty(message))
                ViewBag.Message = message;

            var except = db.Products.Where(p=>p.Customers.Where(c=>c.CustomerId==id).Count()>0);
            return View(new CustomerProductViewModel() 
            {
                CustomerId=customer.CustomerId,
                Customer=customer,
                Products=db.Products.Except(except).ToList(),
            });
        }

        [HandleError]
        public async Task<ActionResult> AddCustomerProduct(int productId, int customerId)
        {
            Customer customer = await db.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }

            Product product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            db.CustomerProducts.Add(new CustomerProduct()
            {
                Active = true,
                CustomerId = customerId,
                ProductId=productId,
                Date=DateTime.Today,
            });

            await db.SaveChangesAsync();
            return RedirectToAction("CreateCustomerProduct", new { id=customerId,message="Save success!!!"});
        }       

        [HandleError]
        public async Task<ActionResult> EditCustomerProduct(int? id)
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

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomerProduct(CustomerProduct customerProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (customerProduct.StatePurchase==StatePurchase.Entregado)
                {
                    var product = db.Products.Find(customerProduct.ProductId);
                    if(product.Unique)
                    {
                        product.State = State.Agotado;
                        db.Entry(product).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Details", new { id = customerProduct.CustomerId });
            }
        
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", customerProduct.ProductId);
            return View(customerProduct);
        }

        [HandleError]
        public async Task<ActionResult> DeleteCustomerProduct(int? id)
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

            db.CustomerProducts.Remove(customerProduct);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id=customerProduct.CustomerId});
        }

        #endregion

        #region Revenue
        [HandleError]
        public async Task<ActionResult> DetailsRevenue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = await db.Revenues.FindAsync(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
            return View(revenue);
        }

        [HandleError]
        public async Task<ActionResult> CreateRevenue(int? id)
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

            return View(new Revenue()
            {
                CustomerId = customer.CustomerId,
                Customer = customer
            });
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRevenue(Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                revenue.FunctionaryId = db.Functionaries.Where(f => f.UserName == User.Identity.Name).FirstOrDefault().FunctionaryId;
                db.Revenues.Add(revenue);
                await db.SaveChangesAsync();
                await AddRevenue(revenue);
                return RedirectToAction("Details", new { id = revenue.CustomerId });
            }

            return View(revenue);
        }

        private async Task AddRevenue(Revenue revenue)
        {
            var customer = await db.Customers.FindAsync(revenue.CustomerId);
            switch (revenue.RevenueType)
            {
                case RevenueType.Short:
                    customer.ShortRevenue += revenue.Amount;
                    break;
                case RevenueType.Medium:
                    customer.MediumRevenue += revenue.Amount;
                    break;
                case RevenueType.Long:
                    customer.LongRevenue += revenue.Amount;
                    break;
                default:
                    break;
            }
            db.Entry(customer).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        [HandleError]
        public async Task<ActionResult> EditRevenue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = await db.Revenues.FindAsync(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }

            return View(revenue);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRevenue(Revenue revenue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revenue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = revenue.CustomerId });
            }

            return View(revenue);
        }

        [HandleError]
        public async Task<ActionResult> DeleteRevenue(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Revenue revenue = await db.Revenues.FindAsync(id);
            if (revenue == null)
            {
                return HttpNotFound();
            }
   
            db.Revenues.Remove(revenue);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = revenue.CustomerId });
        }
        #endregion

        #region Functions
        public bool ChangeRevenue(double value, string type, int id)
        {
            try
            {
                var customer = db.Customers.Find(id);
                switch (type)
                {
                    case "s":
                        customer.ShortRevenue = value;
                        break;
                    case "m":
                        customer.MediumRevenue = value;
                        break;
                    case "l":
                        customer.LongRevenue = value;
                        break;
                    default:
                        break;
                }
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion

        #region CustomerInvestor
        [HandleError]
        public async Task<ActionResult> DetailsCustomerInvestor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInvestor customerInvestor = await db.CustomerInvestors.FindAsync(id);
            if (customerInvestor == null)
            {
                return HttpNotFound();
            }
            return View(customerInvestor);
        }

        [HandleError]
        public async Task<ActionResult> CreateCustomerInvestor(int? id)
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

            ViewBag.InvestorId = new SelectList(db.Customers, "CustomerId", "AllName");

            return View(new CustomerInvestor { CustomerId=customer.CustomerId, Customer=customer });
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomerInvestor(CustomerInvestor customerInvestor)
        {
            if (ModelState.IsValid)
            {
                db.CustomerInvestors.Add(customerInvestor);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = customerInvestor.CustomerId });
            }
            ViewBag.InvestorId = new SelectList(db.Customers, "CustomerId", "AllName", customerInvestor.InvestorId);
            customerInvestor.Customer = db.Customers.Find(customerInvestor.CustomerId);
            return View(customerInvestor);
        }

        [HandleError]
        public async Task<ActionResult> EditCustomerInvestor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInvestor customerInvestor = await db.CustomerInvestors.FindAsync(id);
            if (customerInvestor == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvestorId = new SelectList(db.Customers, "CustomerId", "AllName", customerInvestor.InvestorId);
            return View(customerInvestor);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomerInvestor(CustomerInvestor customerInvestor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerInvestor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { id = customerInvestor.CustomerId });
            }
            ViewBag.InvestorId = new SelectList(db.Customers, "CustomerId", "AllName", customerInvestor.InvestorId);
            customerInvestor.Customer = db.Customers.Find(customerInvestor.CustomerId);
            return View(customerInvestor);
        }

        [HandleError]
        public async Task<ActionResult> DeleteCustomerInvestor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInvestor customerInvestor = await db.CustomerInvestors.FindAsync(id);
            if (customerInvestor == null)
            {
                return HttpNotFound();
            }

            db.CustomerInvestors.Remove(customerInvestor);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { id = customerInvestor.CustomerId });
        }
        #endregion

        #region InvestorProduct
        [HandleError]
        public async Task<ActionResult> DetailsInvestorProduct(int? id)
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
        public async Task<ActionResult> CreateInvestorProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerInvestor investor = await db.CustomerInvestors.FindAsync(id);
            if (investor == null)
            {
                return HttpNotFound();
            }
            var investorProduct = new InvestorProduct
            {
                Active = true,
                CustomerInvestorId = investor.CustomerInvestorId,
                Investor = investor,
                PercentageProfit = 0,                
            };
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View(investorProduct);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInvestorProduct(InvestorProduct investorProduct)
        {
            if (ModelState.IsValid)
            {
                db.InvestorProducts.Add(investorProduct);
                await db.SaveChangesAsync();
                var investor = db.CustomerInvestors.Find(investorProduct.CustomerInvestorId);
                return RedirectToAction("Details", new { id = investor.CustomerId });
            }

            ViewBag.CustomerInvestorId = new SelectList(db.CustomerInvestors, "CustomerInvestorId", "CustomerInvestorId", investorProduct.CustomerInvestorId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        public async Task<ActionResult> EditInvestorProduct(int? id)
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
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInvestorProduct(InvestorProduct investorProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investorProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                var investor = db.CustomerInvestors.Find(investorProduct.CustomerInvestorId);
                return RedirectToAction("Details", new { id = investor.CustomerId });
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", investorProduct.ProductId);
            return View(investorProduct);
        }

        [HandleError]
        public async Task<ActionResult> DeleteInvestorProduct(int? id)
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
            var investor = db.CustomerInvestors.Find(investorProduct.CustomerInvestorId);
            return RedirectToAction("Details", new { id = investor.CustomerId });
        }

        #region Function
        public string GetInvestorProduct(int? id)
        {
            var result = "<table>";
            result += "<tr><th>Picture</th><th>Producto</th><th>% Profit</th><th></th></tr>";
            if (id == null)
            {
                return "<h2>No existen datos (" + id + ")</h2>";
            }
            var investor =  db.CustomerInvestors.Find(id);
            if (investor == null)
            {
                return "<h2>No existen datos  (" + id + ")</h2>";
            }
            foreach (var item in investor.Products)
            {
                result += "<tr>";
                result += "<td><img src='" + item.Product.FullRoutePicture + "' alt='Picture Profile' class='img-thumbnail rounded-circle img-profile-sm' /></td>";
                result += "<td>" + item.Product.Name + "</td>";
                result += "<td>" + item.PercentageProfit + "% </td>";
                result += "<td><a href='/Customers/EditInvestorProduct/" + item.CustomerInvestorId  + "' class='btn btn-sm btn-warning'><span class='fa fa-pencil'></span>&nbsp;Edit</a>" +
                             " <a href='/Customers/DeleteInvestorProduct/" + item.CustomerInvestorId + "' class='btn btn-sm btn-danger text-light'><span class='fa fa-remove'></span>&nbsp;Delete</a></td>";
               result += "</tr>";
            }

            result = result != "<table>" ? (result + "</table>") : "<h2>No existen datos  (" + id + ")</h2>";
            return result;           
        }
        #endregion
        #endregion

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
