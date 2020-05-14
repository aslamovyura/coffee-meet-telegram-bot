using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common;
using Core.Models;

namespace Core.Interfaces
{
    /// <summary>
    /// Interface for manager of application users.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Get user by name.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <returns>Application user.</returns>
        Task<AppUser> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Get user by identifier.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Application user.</returns>
        Task<AppUser> GetUserByIdAsync(long userId);

        /// <summary>
        /// Get all application users.
        /// </summary>
        /// <returns>Array of application users.</returns>
        Task<ICollection<AppUser>> GetUsersAsync();

        /// <summary>
        /// Create new application user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="username">User name.</param>
        /// <param name="firstName">User first name.</param>
        /// <param name="lastName">User last name.</param>
        /// <returns>Operation result.</returns>
        Task<Result> CreateUserAsync(long userId, string username, string firstName, string lastName);

        /// <summary>
        /// Create application user by id.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>Operation result.</returns>
        Task<Result> DeleteUserAsync(long userId);
    }
}