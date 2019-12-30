namespace Chavo.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Web.Mvc;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} is requerido")]
        [MaxLength(200, ErrorMessage = "The lenght {0} is {1} caracters")]
        [Display(Name = "Product")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is requerido")]
        [MaxLength(200, ErrorMessage = "The lenght {0} is {1} caracters")]
        [Index("Product_Code_Index", IsUnique = true)]
        public string Code { get; set; }

        public string Serie { get; set; }

        public string Model { get; set; }

        [Display(Name = "Age")]
        public string Age { get; set; }

        public double Weight { get; set; } = 0;

        public int? WeightId { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Picture { get; set; }

        [NotMapped]
        public string FullRoutePicture
        {
            get
            {
                if (!string.IsNullOrEmpty(Picture))
                {
                    return string.Concat("http://djarquin01-002-site1.1tempurl.com", Picture.Substring(1));
                }
                return string.Empty;
            }
        }

        [AllowHtml]
        [Display(Name = "Short Description")]
        [DataType(DataType.MultilineText)]
        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Cost Amount")]
        public double CostAmount { get; set; }

        [Display(Name = "Price Amount")]
        public double PriceAmount { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; } = 0;

        [Display(Name = "Discount Amount")]
        [NotMapped]
        public double DiscountAmount { get { return (double)Math.Round((decimal)Discount == 0 ? PriceAmount : (double)((decimal)PriceAmount - (decimal)((decimal)PriceAmount * (decimal)((decimal)Discount / (decimal)100))) / 100d, 0) * 100; } }

        public DateTime Date { get; set; } = DateTime.Today;

        public string Video { get; set; }

        public int SubCategoryId { get; set; }

        public int CurrencyId { get; set; }

        public int? UnitLengthId { get; set; }

        public bool Active { get; set; } = true;

        public virtual SubCategory SubCategory { get; set; }

        public virtual Weight WeightType { get; set; }

        public virtual UnitLength UnitLength { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<CustomerProduct> Customers { get; set; }

        public Product()
        {
            Date = DateTime.Today;
            Active = true;
        }
    }
}