using System.Data;

namespace Example.DataAccess.Infrastructure
{
    /// <summary>
    /// This class represents a session with the Database and provides Connection and Transaction for DB operations.
    /// </summary>
    public interface IDatabaseContext : IDisposable
    {
        /// <summary>
        /// The current Database Connection.
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// The current Transaction.
        /// </summary>
        IDbTransaction Transaction { get; }
    }
}
