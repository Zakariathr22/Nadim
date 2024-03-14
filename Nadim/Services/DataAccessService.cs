using MySqlConnector;
using System;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public sealed class DataAccessService : IDisposable
    {
        private MySqlConnection _connection;
        private readonly string _connectionString;

        private DataAccessService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static DataAccessService CreateInstance(string connectionString)
        {
            return new DataAccessService(connectionString);
        }

        public async Task OpenConnectionAsync()
        {
            _connection = new MySqlConnection(_connectionString);
            await _connection.OpenAsync();
        }

        public async Task CloseConnectionAsync()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                await _connection.CloseAsync();
                _connection.Dispose();
            }
        }

        public async Task<object> ExecuteScalarAsync(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return await command.ExecuteScalarAsync();
        }

        public async Task<MySqlDataReader> ExecuteQueryAsync(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return await command.ExecuteReaderAsync();
        }

        public async Task<int> ExecuteNonQueryAsync(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return await command.ExecuteNonQueryAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
