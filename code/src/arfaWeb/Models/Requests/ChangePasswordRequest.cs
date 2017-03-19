using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models.Requests
{
    public class ChangePasswordRequest : AuthorizedRequest
    {
        public string username { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
