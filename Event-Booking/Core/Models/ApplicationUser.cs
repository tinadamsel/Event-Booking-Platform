
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Core.DB;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }
        [Display(Name = "Other Name")]

        public string Name => FirstName + " " + LastName;
        [Display(Name = "Date Registered")]
        public DateTime DateRegistered { get; set; }
        public bool IsAdmin { get; set; }
        public bool Deactivated { get; set; }


    }

}
