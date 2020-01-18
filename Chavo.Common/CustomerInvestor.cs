namespace Chavo.Common
{
    using System;
    using System.Collections.Generic;

    public class CustomerInvestor
    {
        public int CustomerInvestorId { get; set; }

        public int CustomerId { get; set; }

        public int InvestorId { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        public bool Active { get; set; } = true;

        public virtual Customer Customer { get; set; }

        public virtual Customer Investor { get; set; }

        public virtual ICollection<InvestorProduct> Products { get; set; }

        public CustomerInvestor()
        {
            Active = true;
            Date = DateTime.Today;
        }
    }
}