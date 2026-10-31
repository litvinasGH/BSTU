using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printinvest.other
{
    public class User
    {
        public enum role_enum
        {
            Admin,
            Client
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public role_enum Role { get; set; } 
    }
}
