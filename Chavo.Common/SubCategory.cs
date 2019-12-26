namespace Chavo.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;

    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "{0} is requerid")]
        [MaxLength(50, ErrorMessage = "Length {0} is {1} caracters")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }

        public int CategoryId { get; set; }

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

        public bool Active { get; set; } = true;

        public virtual ICollection<Product> Products { get; set; }

        public virtual Category Category { get; set; }

        public SubCategory()
        {
            Active = true;
        }
    }
}