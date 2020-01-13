namespace Chavo.Common
{
    using System.ComponentModel.DataAnnotations;

    public class GeneralConfiguration
    {
        [Key]
        public int GeneralConfigurationId { get; set; }

        [Display(Name = "Company")]
        public string Name { get; set; }

        public string Logo { get; set; }

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
        public string CashingConditions { get; set; }

        [Display(Name = "Video Cashing")]
        public string VideoCashing { get; set; }

        [Display(Name = "Real Estate")]
        public string RealEstate { get; set; }

        [Display(Name = "Select Clothes")]
        public string SelectClothes { get; set; }

        public string Chat { get; set; }

        [Display(Name = "Transacction Number")]
        public string TransacctionNumber { get; set; }

        [Display(Name = "Explication")]
        public string DescriptionTransacctionNumber { get; set; }

        [Display(Name = "Message Login")]
        public string MessageLogin { get; set; }

        [Display(Name = "Message Footer")]
        public string MessageFooter { get; set; }
    }
}