using Example.Models;

namespace Example.BusinessLogic.Infrastructure
{
    /// <summary>
    /// The service to work with the User entity.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get the User by Id.
        /// </summary>
        /// <param name="id">The Id of the User.</param>
        /// <returns>The User with provided Id.</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Get all existing Users.
        /// </summary>
        /// <returns>All existing Users.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Add new User.
        /// </summary>
        /// <param name="user">The User model.</param>
        /// <returns>The Id of the new User.</returns>
        Task<int> AddUserAsync(User user);

        /// <summary>
        /// Update existing User.
        /// </summary>
        /// <param name="user">The User model.</param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Delete User by Id.
        /// </summary>
        /// <param name="id">The Id of the User to delete.</param>
        /// <returns></returns>
        Task DeleteUserAsync(int id);
    }
}
