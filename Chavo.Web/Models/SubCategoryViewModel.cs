namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class SubCategoryViewModel : SubCategory
    {
        public HttpPostedFileBase PictureFile { get; set; }

    }
}