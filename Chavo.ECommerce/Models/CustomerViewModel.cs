namespace Chavo.ECommerce.Models
{
    using Common;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class CustomerViewModel:Customer
    {
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Birth Date")]
        public string BirthDateString { get; set; }

        public GeneralConfiguration GeneralConfigurations { get; set; }
    }
}