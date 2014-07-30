using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MJ.Web.Admin.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }

    public class UserRegisterModel
    {

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string PwdSalt { get; set; }

        public Guid UserTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool NeedsPasswordReset { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        public DateTime DateTimeDeleted { get; set; }

    }

}