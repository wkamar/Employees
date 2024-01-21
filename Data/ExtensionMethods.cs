using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public static class ExtensionMethods
    {
        public static IEnumerable<ApplicationUser> WithoutPasswords(this IEnumerable<ApplicationUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static ApplicationUser WithoutPassword(this ApplicationUser user)
        {
            //user.Password = null;
            return user;
        }
    }
}
