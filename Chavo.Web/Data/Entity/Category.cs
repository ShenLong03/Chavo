namespace Chavo.Web.Data.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Configuration;

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
        public string FullRoutePicture
        {
            get
            {
                if (!string.IsNullOrEmpty(Picture))
                {
                    return string.Concat(WebConfigurationManager.AppSettings["CPanelPath"], Picture.Substring(1));
                }
                return string.Empty;
            }
        }

        public bool Active { get; set; } = true;

        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public Category()
        {
            Active = true;
        }
    }
}