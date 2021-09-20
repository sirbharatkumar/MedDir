using MedDir.Domain.Entities;
using MedDir.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedDir.Domain.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Hardcoded the User for demo purpose
        /// </summary>
        private List<ApiAuthUser> _users = new List<ApiAuthUser>
        {
            new ApiAuthUser { Id = 1, Name = "API Auth User", Username = "340d11ef14874a61a169c1c169a2815e", Password = "c36fef5d388e4b6ba1b53ae78d257f26" }
        };

        /// <summary>
        /// Authenticate the User before giving access to the API
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiAuthUser> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApiAuthUser>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
