using MedDir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedDir.Domain.Extensions
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Remove the password for a given set of Users
        /// </summary>
        /// <param name="users">User list for which password field needs to be removed</param>
        /// <returns>User List with removed password</returns>
        public static IEnumerable<ApiAuthUser> WithoutPasswords(this IEnumerable<ApiAuthUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        /// <summary>
        /// Remove the password for a given user
        /// </summary>
        /// <param name="users">User for which password field needs to be removed</param>
        /// <returns>User with removed password</returns>
        public static ApiAuthUser WithoutPassword(this ApiAuthUser user)
        {
            user.Password = null;
            return user;
        }
    }
}
