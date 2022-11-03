using MySqlConnector;
using System.Data;

namespace EcoCar.API.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new MySqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public IDbConnection NewConnectionIsolated()
        {
            return new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IDbTransaction Begin()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public bool Commit()
        {
            bool r = false;

            try
            {
                if (_transaction.Connection != null)
                {
                    _transaction.Commit();
                    r = true;
                }
            }
            catch
            {
                _transaction.Rollback();
                r = false;
            }
            finally
            {
                _connection.Close();
                _transaction.Dispose();
            }

            return r;
        }

        public bool IsConnectionOpen()
        {
            return (Connection.State & ConnectionState.Open) != 0;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _transaction?.Dispose();
            _connection = null;
            _transaction = null;
        }
    }
}