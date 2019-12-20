﻿namespace Chavo.Web.Data.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerProduct
    {
        [Key]
        public int CustomerProductId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        public bool Active { get; set; } = true;

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public CustomerProduct()
        {
            Date = DateTime.Today;
            Active = true;
        }
    }
}