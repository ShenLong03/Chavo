namespace Chavo.Web.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "ID")]
        public string ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string AllName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDate { get; set; }

        public string Picture { get; set; }

        [NotMapped]
        public string FullRoutePicture
        {
            get
            {
                if (!string.IsNullOrEmpty(Picture))
                {
                    return string.Concat(WebConfigurationManager.AppSettings["RutComplet"], Picture.Substring(1));
                }
                return string.Empty;
            }
        }

        public double? Credit { get; set; } = 0;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Email { get; set; }

        public string UserName { get; set; }

        public int? Points { get; set; } = 0;

        public bool Active { get; set; } = true;

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } = DateTime.Today;

        public Genero Genero { get; set; }

        [Display(Name = "Short Revenue")]
        public double ShortRevenue { get; set; } = 0;

        [Display(Name = "Medium Revenue")]
        public double MediumRevenue { get; set; } = 0;

        [Display(Name = "Long Revenue")]
        public double LongRevenue { get; set; } = 0;

        public virtual ICollection<CustomerProduct> Products { get; set; }

        public virtual ICollection<Revenue> Revenues { get; set; }

        public Customer()
        {
            Active = true;
            ShortRevenue = 0;
            MediumRevenue = 0;
            LongRevenue = 0;
        }
    }

    public enum Genero
    {
        Male, Female
    }
}