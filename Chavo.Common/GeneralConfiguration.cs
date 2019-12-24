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
    }
}