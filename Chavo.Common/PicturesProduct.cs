namespace Chavo.Common
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;

    public class PicturesProduct
    {
        [Key]
        public int PicturesProductId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        [NotMapped]
        public string FullRoutePicture
        {
            get
            {
                if (!string.IsNullOrEmpty(Picture))
                {
                    return string.Concat(ConfigurationManager.AppSettings["CPanelPath"], Picture.Substring(1));
                }
                return string.Empty;
            }
        }

        public bool Active { get; set; } = true;

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public PicturesProduct()
        {
            Active = true;
        }
    }
}