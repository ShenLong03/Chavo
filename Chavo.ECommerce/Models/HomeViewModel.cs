using Chavo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chavo.ECommerce.Models
{
    public class HomeViewModel
    {
        public string VideoBanner { get; set; }

        public bool DisplayClothes { get; set; }

        public GeneralConfiguration GeneralConfiguration { get; set; }

        public List<Category> Categories { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        public List<Product> Products { get; set; }
    }
}