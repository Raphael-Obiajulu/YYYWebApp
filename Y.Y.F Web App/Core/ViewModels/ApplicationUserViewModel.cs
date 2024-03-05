using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public bool Deactivated { get; set; }
        public DateTime DateCreated { get; set; }
        public int GenderId { get; set; }
        public virtual CommonDropdown? Gender { get; set; }
    }
}
