using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arfa.Interface.Models;

namespace arfaWeb.Models
{
    public class User
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public static User FromInterface(arfa.Interface.Models.User owner)
        {
            return new User()
            {
                firstName = owner.Firstname,
                lastName = owner.Lastname,
                userName = owner.UserName
            };
        }
    }
}
