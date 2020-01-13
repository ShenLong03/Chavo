namespace Chavo.Web.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
                    return string.Concat("http://djarquin01-002-site1.1tempurl.com", Picture.Substring(1));
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

        [Display(Name = "Display Short Revenue")]
        public bool DisplayShortRevenue { get; set; } = true;

        [Display(Name = "Short Revenue")]
        public double ShortRevenue { get; set; } = 0;

        [Display(Name = "Display Medium Revenue")]
        public bool DisplayMediumRevenue { get; set; } = true;

        [Display(Name = "Medium Revenue")]
        public double MediumRevenue { get; set; } = 0;

        [Display(Name = "Display Long Revenue")]
        public bool DisplayLongRevenue { get; set; } = true;

        [Display(Name = "Long Revenue")]
        public double LongRevenue { get; set; } = 0;

        [Display(Name = "Display Clothes")]
        public bool DisplayClothes { get; set; } = true;

        [Display(Name = "Display Inversor")]
        public bool DisplayInversor { get; set; } = true;

        [Display(Name="Max to win")]
        public double MaxToWin { get; set; }

        [DataType(DataType.MultilineText)]
        public string Sizes { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentaries { get; set; }

        public virtual ICollection<CustomerProduct> Products { get; set; }

        public virtual ICollection<Revenue> Revenues { get; set; }

        public virtual ICollection<FirstLogin> FirstLogins { get; set; }

        public Customer()
        {
            Active = true;
            ShortRevenue = 0;
            MediumRevenue = 0;
            LongRevenue = 0;
            DisplayShortRevenue = true;
            DisplayMediumRevenue = true;
            DisplayLongRevenue = true;
            DisplayClothes = true;
            DisplayInversor = true;
        }
    }

    public enum Genero
    {
        Male, Female
    }
}