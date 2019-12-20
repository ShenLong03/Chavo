namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class FunctionaryViewModel : Functionary
    {
        public HttpPostedFileBase PictureFile { get; set; }

        [Display(Name = "Birth Date")]
        public string BirthDateString { get; set; }
    }
}