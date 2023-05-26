using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public abstract class Database
    {
        private string _connectionString;

        protected Database()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["schedulingDb"].ConnectionString;
        }

        protected MySqlConnection OpenConnection()
        {
            // used by calling using (connection = OpenConnection()) to handle closing automatically after block
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection;
        }
    }
}
