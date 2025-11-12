using ApiBackendParkingApp.Interfaces;
using ApiBackendParkingApp.Utilis;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ApiBackendParkingApp.Repositories
{
    public class DbInteraction : IDbInteractions
    {
        private string _dbServer;

        private string _dataBaseName; 

        private bool _dbTrustedConnection;

        private bool _dbTrustServerCertificate; 
        public DbInteraction(IDataBaseConfig dataBaseconfig) 
        {
            _dbServer = dataBaseconfig.DbServer;
            _dataBaseName = dataBaseconfig.DataBaseName;
            _dbTrustedConnection = dataBaseconfig.DbTrustedConnection;
            _dbTrustServerCertificate = dataBaseconfig.DbTrustServerCertificate;
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed)
                return;

            _dbServer = null;
            _dataBaseName = null;
            _disposed = true;
        }

        private SqlConnection GetConnection()
        {
            var connectionBuilder = new SqlConnectionStringBuilder
            {
                DataSource = _dbServer,
                InitialCatalog = _dataBaseName,
                IntegratedSecurity = _dbTrustedConnection,
               TrustServerCertificate = _dbTrustServerCertificate
            };
            var connectionString = connectionBuilder.ConnectionString;
            return new SqlConnection(connectionString);
        }


        public int Execuce(string sql, object param =null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = connection.Execute(sql, param);
            return result; 
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.ExecuteAsync(sql, param);
            return result;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = connection.Query<T>(sql, param);
            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.QueryAsync<T>(sql, param);
            return result;
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result =  connection.QueryFirstOrDefault<T>(sql, param);
            return result;
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            using var connection = GetConnection();
            connection.Open();
            var result = await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            return result;
        }
    }
}
