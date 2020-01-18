namespace Chavo.Common
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


        [Display(Name = "State Purchase")]
        public StatePurchase StatePurchase { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

        public CustomerProduct()
        {
            Date = DateTime.Today;
            Active = true;
        }
    }

    public enum StatePurchase
    {
        Comprado, Entregado
    }
}