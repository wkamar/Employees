using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class ApplicationRole : IdentityRole<int>
    {

    }
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {

    }
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {

    }
    public class ApplicationUserRole : IdentityUserRole<int>
    {

    }
    public class ApplicationUserToken : IdentityUserToken<int>
    {

    }
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {

    }

    public class ApplicationUser : IdentityUser<int>
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string UName { get; set; }

        //public string Password { get; set; }

        //public string Token { get; set; }
    }
}
