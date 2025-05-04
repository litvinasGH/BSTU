using System;

namespace Printinvest.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhotoPath { get; set; }
        public bool IsAdmin { get; set; }
    }
}