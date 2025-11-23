using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
   public class ApplicationUserViewModel
        {
            public string Id { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string Username { get; set; }
            public virtual string Email { get; set; }
            public virtual string Phonenumber { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public DateTime DateRegistered { get; set; }
            public bool IsAdmin { get; set; }
            public bool Deactivated { get; set; }
        }
    
}

