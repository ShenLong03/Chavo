namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class CustomerProductViewModel : CustomerProduct
    {
        public List<Product> Products { get; set; }
    }
}