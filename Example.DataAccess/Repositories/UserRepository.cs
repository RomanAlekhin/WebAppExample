using Dapper;

using Example.DataAccess.Infrastructure;
using Example.Models;

namespace Example.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public UserRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<User> GetAsync(int id)
        {
            using (var connection = _databaseContext.Connection)
            {
                return connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM [User] WHERE Id = @Id", new { Id = id });
            }
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = _databaseContext.Connection)
            {
                return connection.QueryAsync<User>("SELECT * FROM [User]");
            }
        }

        public async Task<int> AddAsync(User user)
        {
            using (var connection = _databaseContext.Connection)
            using (var transaction = _databaseContext.Transaction)
            {
                int? newUserId;

                try
                {
                    newUserId = await connection.InsertAsync(user, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to create User. Transaction rolled back.", ex);
                }

                if (!newUserId.HasValue || newUserId.Value <= 0)
                {
                    throw new Exception("Failed to create User. The Id of the new User isn't more than 0");
                }

                return newUserId.Value;
            }
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = _databaseContext.Connection)
            using (var transaction = _databaseContext.Transaction)
            {
                try
                {
                    await connection.UpdateAsync(user, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to update User. Transaction rolled back.", ex);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = _databaseContext.Connection)
            using (var transaction = _databaseContext.Transaction)
            {
                try
                {
                    await connection.DeleteAsync<User>(id, transaction).ConfigureAwait(false);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Failed to delete User. Transaction rolled back.", ex);
                }
            }
        }
    }
}
