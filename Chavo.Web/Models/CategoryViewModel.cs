namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class CategoryViewModel : Category
    {
        public HttpPostedFileBase PictureFile { get; set; }

    }
}