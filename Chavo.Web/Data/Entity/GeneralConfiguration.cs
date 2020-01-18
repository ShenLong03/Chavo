namespace Chavo.Web.Data.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class GeneralConfiguration
    {
        [Key]
        public int GeneralConfigurationId { get; set; }

        [Display(Name = "Company")]
        public string Name { get; set; }

        public string Logo { get; set; }


        [NotMapped]
        public string FullRouteLogo
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

        [Display(Name = "Video Banner")]
        public string VideoBanner { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Facebook { get; set; }

        [Display(Name = "Facebook Group")]
        public string FacebookGroup { get; set; }

        public string Instagram { get; set; }

        public string Twiter { get; set; }

        [Display(Name = "Learning Zone")]
        public string LearningZone { get; set; }

        [Display(Name = "Acquire User Learning Zone")]
        public string AcquireUserLearningZone { get; set; }

        public string Cashing { get; set; }

        [Display(Name = "Cashing Conditions")]
        [DataType(DataType.MultilineText)]
        public string CashingConditions { get; set; }

        [Display(Name = "Video Cashing")]
        public string VideoCashing { get; set; }

        [Display(Name = "Select Clothes")]
        public string SelectClothes { get; set; }

        [Display(Name = "Real Estate")]
        public string RealEstate { get; set; }

        public string Chat { get; set; }

        [Display(Name = "Transacction Number")]
        public string TransacctionNumber { get; set; }

        [Display(Name = "Explication")]
        public string DescriptionTransacctionNumber { get; set; }

        [Display(Name = "Message Login")]
        public string MessageLogin { get; set; }

        [Display(Name = "Message Footer")]
        public string MessageFooter { get; set; }

        [Display(Name = "Message Contact Page")]
        public string MessageContactPage { get; set; }

        [Display(Name = "Email Register Approved")]
        public string EmailRegisterApproved { get; set; }

        [Display(Name = "Email Confirmed")]
        public string EmailConfirmed { get; set; }

        [Display(Name = "Change Password")]
        public string ChangePassword { get; set; }
    }
}