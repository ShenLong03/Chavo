namespace Chavo.Web.Data.Entity
{
    using System.ComponentModel.DataAnnotations;

    public class Register
    {
        [Key]
        public int RegisterId { get; set; }

        public string Email { get; set; }

        public string Transaction { get; set; }

        public string Password { get; set; }

        public bool Approved { get; set; } = false;

        public bool ChangePassword { get; set; } = false;
    }
}