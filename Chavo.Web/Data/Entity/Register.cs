namespace Chavo.Web.Data.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

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