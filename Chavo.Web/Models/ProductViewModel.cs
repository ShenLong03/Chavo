namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class ProductViewModel : Product
    {
        public HttpPostedFileBase PictureFile { get; set; }

        public int CategoryId { get; set; }
    }
}