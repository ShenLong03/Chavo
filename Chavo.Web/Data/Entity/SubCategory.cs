namespace Chavo.Web.Data.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Configuration;

    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El tamaño maximo del campo {0} es {1} caracteres")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
        public string PictureRuthComplete { get { return string.Concat(WebConfigurationManager.AppSettings["RutComplet"], Picture.Substring(1)); } }

        public bool Active { get; set; } = true;

        public virtual ICollection<Product> Products { get; set; }

        public virtual Category Category { get; set; }

        public SubCategory()
        {
            Active = true;
        }
    }
}