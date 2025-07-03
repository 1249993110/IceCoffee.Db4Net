using System.Data;
using System.Data.Common;

namespace IceCoffee.Db4Net
{
    /// <summary>
    /// Represents a unit of work, managing a database connection and transaction.
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbTransaction _transaction;
        private bool _committed = false;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        public UnitOfWork(DbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified isolation level.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <param name="il">The isolation level for the transaction.</param>
        public UnitOfWork(DbConnection connection, IsolationLevel il)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction(il);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDbConnection DbConnection => _connection;

        /// <summary>
        /// Gets the database transaction.
        /// </summary>
        public IDbTransaction DbTransaction => _transaction;

        /// <summary>
        /// Commits the current transaction.
        /// If the commit fails, the transaction is rolled back.
        /// After committing, the UnitOfWork instance is considered "done" and its transaction/connection will be disposed upon exiting the using block.
        /// </summary>
        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork), "Cannot commit on a disposed UnitOfWork instance.");
            }
            if (_committed)
            {
                throw new InvalidOperationException("Transaction has already been committed.");
            }

            try
            {
                _transaction.Commit();
                _committed = true;
            }
            catch
            {
                throw;
            }
        }

#if NETCOREAPP
        /// <summary>
        /// Asynchronously commits the current transaction.
        /// If the commit fails, the transaction is rolled back.
        /// After committing, the UnitOfWork instance is considered "done" and its transaction/connection will be disposed upon exiting the using block.
        /// </summary>
        public async Task CommitAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork), "Cannot commit on a disposed UnitOfWork instance.");
            }
            if (_committed)
            {
                throw new InvalidOperationException("Transaction has already been committed.");
            }

            try
            {
                await _transaction.CommitAsync();
                _committed = true;
            }
            catch
            {
                throw;
            }
        }
#endif

        /// <summary>
        /// Releases all resources used by the <see cref="UnitOfWork"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="UnitOfWork"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {
                    try
                    {
                        if (_committed == false && _transaction != null)
                        {
                            _transaction.Rollback();
                        }
                    }
                    finally
                    {
                        _transaction.Dispose();

                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                    }
                }

                _disposed = true;
            }
        }
    }
}
