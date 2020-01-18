namespace Chavo.Common
{
    using System.ComponentModel.DataAnnotations;

    public class InvestorProduct
    {
        [Key]
        public int InvestorProductId { get; set; }

        public int CustomerInvestorId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Percentage of profit")]
        public double PercentageProfit { get; set; } = 0;

        public bool Active { get; set; } = true;

        public virtual CustomerInvestor Investor { get; set; }

        public virtual Product Product { get; set; }

        public InvestorProduct()
        {
            PercentageProfit = 0;
            Active = true;
        }
    }
}