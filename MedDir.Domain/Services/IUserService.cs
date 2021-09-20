using MedDir.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedDir.Domain.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticate the user to allow access to the API
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>User</returns>
        Task<ApiAuthUser> Authenticate(string username, string password);

        /// <summary>
        /// Get all the Users
        /// </summary>
        /// <returns>User List</returns>
        Task<IEnumerable<ApiAuthUser>> GetAll();
    }
}
