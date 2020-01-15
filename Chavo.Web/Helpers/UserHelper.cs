namespace Chavo.Web.Helpers
{
    using Chavo.Web.Data;
    using Chavo.Web.Data.Entity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public class UsersHelper : IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Check to see if Role Exists, if not create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        private void AddPermisionsToSuperuser(string userName = "", string role = "")
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(role))
            {
                var user = userManager.FindByName(userName);

                if (!userManager.IsInRole(user.Id, role))
                {
                    userManager.AddToRole(user.Id, role);
                }
            }
        }

        public static async Task Delete(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // id is id of the user to be deleted.
            var user = await userManager.FindByIdAsync(id); //use async find
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                // user is deleted
            }
       
        }

        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CreateUserASP(email, "Administrator", password);
            }
            else
            {
                userManager.AddToRole(userASP.Id, "Administrator");
            }                       
            using (DataContext db = new DataContext())
            {
                if (db.Functionaries.Where(f=>f.UserName==email).Count()==0)
                {
                    db.Functionaries.Add(new Functionary
                    {
                        FirstName = "Donovan",
                        Active = true,
                        LastName = "Jarquin",
                        Email = email,
                        UserName = email,
                        BirthDate = DateTime.Today,
                        CreateDate = DateTime.Today,
                        ID = "",
                    });
                    db.SaveChanges();
                }                
            }           
        }    

        //private static Persona Persona()
        //{
        //    throw new NotImplementedException();
        //}

        public static void CreateUserASP(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string user, string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = user,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static bool ExistUser(string userName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var resp= userManager.FindByName(userName);
            if (resp==null)
            {
                return false;
            }
            return true;
        }

        //public static async Task PasswordRecovery(string email)
        //{
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
        //    var userASP = userManager.FindByEmail(email);
        //    if (userASP == null)
        //    {
        //        return;
        //    }

        //    //var user = db.Personas.Where(tp => tp.Email == email).FirstOrDefault();
        //    //if (user == null)
        //    //{
        //    //    return;
        //    //}

        //    var random = new Random();
        //    var newPassword = string.Format("{0}{1}{2:04}*",
        //        //user.Nombre.Trim().ToUpper().Substring(0, 1),
        //        //user.Apellidos.Trim().ToLower(),
        //        random.Next(10000));

        //    userManager.RemovePassword(userASP.Id);
        //    userManager.AddPassword(userASP.Id, newPassword);

        //    var subject = "Taxes Password Recovery";
        //    var body = string.Format(@"
        //        <h1>Taxes Password Recovery</h1>
        //        <p>Yor new password is: <strong>{0}</strong></p>
        //        <p>Please change it for one, that you remember easyly",
        //        newPassword);

        //    await MailHelper.SendMail(email, subject, body);
        //}

        //public static int Persona(string user)
        //{
        //    using (DataContext dbTemp = new DataContext())
        //    {

        //        if (dbTemp.Personas.Where(e => e.Email == user).ToList().Count() > 0)
        //        {
        //            return dbTemp.Personas.Where(e => e.Email == user).FirstOrDefault().PersonaId;
        //        }

        //        return 0;
        //    }
        //}

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}
