using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Common.DTO
{
    public class SystemUserDto
    {

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PwdSalt { get; set; }

        public Guid UserTypeId { get; set; }

        public bool IsActive { get; set; }

        public bool NeedsPasswordReset { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        public DateTime DateTimeDeleted { get; set; }

    }
}
