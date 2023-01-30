using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace CakesByVern_Data.Database
{
    public class SQLConnector
    {
        private readonly string _connectionString;

        public SQLConnector(IConfiguration configuration)
        {
            bool debug;
            bool.TryParse(configuration["Environment:Development"], out debug);

            string? res = String.Empty;
            if (debug)
                res = configuration.GetConnectionString("DATABASEwebDebugging");
            else
                res = configuration.GetConnectionString("DATABASEweb");

            _connectionString = res!=null?res:String.Empty;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public bool IsConnected()
        {
            try{
                using var conn = GetConnection(); 
                conn.Open();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        // must be disposed
        public MySqlDataReader ExecuteQueryReturn(string query)
        {
            if (!IsConnected()) GetConnection();
            var conn = GetConnection();
            conn.Open();
            return new MySqlCommand(query, conn).ExecuteReader();
        }

        public async Task<MySqlDataReader> ExecuteQueryReturnAsync(string query)
        {
            var conn = GetConnection();
            conn.Open();
            MySqlCommand sqlCommand= new MySqlCommand(query, conn);
            return (MySqlDataReader) await sqlCommand.ExecuteReaderAsync(); 
        }

        public void ExecuteQuery(string query)
        {
            if(!IsConnected()) return;
            var conn = GetConnection();
            conn.Open();
            new MySqlCommand(query, conn).ExecuteNonQuery();
            conn.Close();
        }
    }
}
