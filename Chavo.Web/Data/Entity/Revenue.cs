namespace Chavo.Web.Data.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Revenue
    {
        [Key]
        public int RevenueId { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public RevenueType RevenueType { get; set; }

        public bool Active { get; set; } = true;

        public int CustomerId { get; set; }

        public int FunctionaryId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Functionary Functionary { get; set; }

        public Revenue()
        {
            Active = true;
            Date = DateTime.Today;
        }
    }

    public enum RevenueType
    {
        Short, Medium, Long
    }
}