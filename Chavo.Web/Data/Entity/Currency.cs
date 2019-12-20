namespace Chavo.Web.Data.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [Display(Name = "Currency")]
        public string Name { get; set; }

        public string Nomenclature { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}