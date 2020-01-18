﻿
namespace Chavo.Web.Models
{
    using Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [NotMapped]
    public class GeneralConfigurationViewModel : GeneralConfiguration
    {
        public HttpPostedFileBase LogoFile { get; set; }

        public HttpPostedFileBase PictureFile { get; set; }
    }
}