namespace Chavo.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El tamaño maximo del campo {0} es {1} caracteres")]
        [Display(Name = "Category")]
        [Index("Category_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Picture { get; set; }

        [NotMapped]
        public string PictureRuthComplete { get { return string.Concat(ConfigurationManager.AppSettings["RutComplet"], Picture.Substring(1)); } }

        public bool Active { get; set; } = true;

        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public Category()
        {
            Active = true;
        }
    }
}