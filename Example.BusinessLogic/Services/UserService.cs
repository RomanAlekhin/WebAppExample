using Microsoft.Extensions.Logging;

using Example.BusinessLogic.Infrastructure;
using Example.DataAccess.Infrastructure;
using Example.Models;

namespace Example.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<IUserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<IUserService> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            _logger.LogDebug("{service}: Getting a User with Id {id}", nameof(UserService), id);

            try
            {
                return _userRepository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{service}: An error occurred while getting a User with Id {id}", nameof(UserService), id);
                throw;
            }
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            _logger.LogDebug("{service}: Getting all Users", nameof(UserService));

            try
            {
                return _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{service}: An error occurred while getting all Users", nameof(UserService));
                throw;
            }
        }

        public Task<int> AddUserAsync(User user)
        {
            _logger.LogDebug("{service}: Adding new User ({name}, {age})", nameof(UserService), user.Name, user.Age);

            try
            {
                return _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{service}: An error occurred while adding new User", nameof(UserService));
                throw;
            }
        }

        public Task UpdateUserAsync(User user)
        {
            _logger.LogDebug("{service}: Updating User ({id}, {name}, {age})", nameof(UserService), user.Id, user.Name, user.Age);

            try
            {
                return _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{service}: An error occurred while updating User with Id {id}", nameof(UserService), user.Id);
                throw;
            }
        }

        public Task DeleteUserAsync(int id)
        {
            _logger.LogDebug("{service}: Deleting User with Id {id}", nameof(UserService), id);

            try
            {
                return _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{service}: An error occurred while deleting User with Id {id}", nameof(UserService), id);
                throw;
            }
        }
    }
}
