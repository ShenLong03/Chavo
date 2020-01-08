namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class PicturesProductViewModel : PicturesProduct
    {
        public HttpPostedFileBase PictureFile { get; set; }
    }
}