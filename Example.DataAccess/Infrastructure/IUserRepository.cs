using Example.Models;

namespace Example.DataAccess.Infrastructure
{
    /// <summary>
    /// The repository to work with the User entity.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get the User by Id.
        /// </summary>
        /// <param name="id">The Id of the User.</param>
        /// <returns>The User with provided Id.</returns>
        Task<User> GetAsync(int id);

        /// <summary>
        /// Get all existing Users.
        /// </summary>
        /// <returns>All existing Users.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Add new User.
        /// </summary>
        /// <param name="user">The User model.</param>
        /// <returns>The Id of the new User.</returns>
        Task<int> AddAsync(User user);

        /// <summary>
        /// Update existing User.
        /// </summary>
        /// <param name="user">The User model.</param>
        /// <returns></returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Delete User by Id.
        /// </summary>
        /// <param name="id">The Id of the User to delete.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
