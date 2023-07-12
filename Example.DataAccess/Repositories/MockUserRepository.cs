using Example.DataAccess.Infrastructure;
using Example.Models;

namespace Example.DataAccess.Repositories
{
    /// <summary>
    /// A mock repository class to emulate the work with the User entity.
    /// </summary>
    public class MockUserRepository : IUserRepository
    {
        // This list of mock users should be persistent as the MockUserRepository lifetime is set to Singleton.
        private readonly List<User> _mockUsers = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Age = 25 },
            new User { Id = 2, Name = "Jane Smith", Age = 30 },
            new User { Id = 3, Name = "Bob Johnson", Age = 35 },
            new User { Id = 4, Name = "John Smith", Age = 33 },
            new User { Id = 5, Name = "Jane Johnson", Age = 45 },
            new User { Id = 6, Name = "Bob Doe", Age = 69 },
        };

        private int _nextUserId = 7; // Track the next Id to be set for the added User.

        public Task<User> GetAsync(int id)
        {
            return Task.FromResult(_mockUsers.First(u => u.Id == id));
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_mockUsers);
        }

        public Task<int> AddAsync(User user)
        {
            user.Id = _nextUserId++;
            _mockUsers.Add(user);

            return Task.FromResult(user.Id);
        }

        public Task UpdateAsync(User user)
        {
            var existingUser = _mockUsers.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                throw new Exception($"Could not find User with Id {user.Id}");
            }

            existingUser.Name = user.Name;
            existingUser.Age = user.Age;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var existingUser = _mockUsers.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                throw new Exception($"Could not find User with Id {id}");
            }

            _mockUsers.Remove(existingUser);

            return Task.CompletedTask;
        }
    }
}
