namespace Chavo.Common
{
    using System.ComponentModel.DataAnnotations;

    public class FirstLogin
    {
        [Key]
        public int FirstLoginId { get; set; }

        public string UserName { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}