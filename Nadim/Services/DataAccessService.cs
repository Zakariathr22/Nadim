using MySqlConnector;
using System;
using System.Net.NetworkInformation;
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

        public void OpenConnection()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public object ExecuteScalar(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return command.ExecuteScalar();
        }

        public MySqlDataReader ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return command.ExecuteReader();
        }

        public int ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public bool IsConnectionAvailable()
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send("127.0.0.1", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ConnectionStatIsOpened()
        {

            if (_connection.State == System.Data.ConnectionState.Open) 
                return true;
            else return false;
        }
    }
}
