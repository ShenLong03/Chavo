namespace Chavo.Web.Data.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Weight
    {
        [Key]
        public int WeightId { get; set; }

        [Display(Name = "Weight")]
        public string Name { get; set; }

        public string Nomenclature { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}