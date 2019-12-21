using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chavo.Common
{
    public class UnitLength
    {
        [Key]
        public int UnitLengthId { get; set; }

        [Display(Name = "UnitLength")]
        public string Name { get; set; }

        public string Nomenclature { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}