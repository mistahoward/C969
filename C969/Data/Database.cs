using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace C969.Data
{
    /// <summary>
    /// Abstract class representing a database.
    /// </summary>
    public abstract class Database
    {
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the Database class.
        /// </summary>
        /// <remarks>
        /// This constructor reads the connection string from the configuration file and assigns it to the
        /// private _connectionString field of the database.
        /// </remarks>
        protected Database()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["schedulingDb"].ConnectionString;
        }

        /// <summary>
        /// Opens a connection to the database.
        /// </summary>
        /// <returns>An open MySqlConnection object.</returns>
        /// <remarks>
        /// This method creates a new connection to the database using the connection string
        /// contained in the _connectionString field, opens the connection, and returns the MySqlConnection object.
        /// </remarks>
        protected MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Retrieves data from a specified table based on a set of columns.
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve data from.</param>
        /// <param name="columns">The list of columns to retrieve data from.</param>
        /// <returns>A MySqlDataReader object containing the retrieved data.</returns>
        /// <exception cref="MySqlException">Thrown when an error occurs executing the query.</exception>
        /// <remarks>
        /// This method constructs a SELECT query using the specified table name and columns and executes it using
        /// the MySqlConnection object returned by the OpenConnection method. It returns a MySqlDataReader object
        /// containing the results of the query.
        /// </remarks>
        protected MySqlDataReader RetrieveData(string tableName, string[] columns)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                string columnsString = string.Join(",", columns);
                string query = $"SELECT {columnsString} FROM {tableName}";
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    return command.ExecuteReader();
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

        /// <summary>
        /// Adds a new row to a specified table.
        /// </summary>
        /// <param name="tableName">The name of the table to insert data into.</param>
        /// <param name="data">The data to insert into the table.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        /// <exception cref="MySqlException">Thrown when an error occurs executing the query.</exception>
        /// <remarks>
        /// This method constructs an INSERT query using the specified table name and data and executes it using
        /// the MySqlConnection object returned by the OpenConnection method. It returns 'true' if the command
        /// executes successfully, otherwise 'false'.
        /// </remarks>
        /// 
        /// <example>
        /// This sample demonstrates how to use the AddData method.
        ///
        /// <code>
        /// var db = new DatabaseImpl();
        /// var data = new Dictionary&lt;string,string&gt;();
        /// data.Add("id", "1");
        /// data.Add("name", "item1");
        /// bool result = db.AddData("myTable", data);
        /// </code>
        /// </example>
        protected bool AddData(string tableName, Dictionary<string, string> data)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                string columns = string.Join(", ", data.Keys);
                string values = string.Join(", ", data.Values);

                string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

        /// <summary>
        /// Updates data in a specified table based on a condition.
        /// </summary>
        /// <param name="tableName">The name of the table to update data in.</param>
        /// <param name="columns">The list of columns to update.</param>
        /// <param name="values">The new values to set for the specified columns.</param>
        /// <param name="condition">The condition used to determine which rows to update.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        /// <exception cref="MySqlException">Thrown when an error occurs executing the query.</exception>
        /// <remarks>
        /// This method constructs an UPDATE query using the specified table name, columns, values, and condition
        /// and executes it using the MySqlConnection object returned by the OpenConnection method. It returns 'true'
        /// if the command executes successfully, otherwise 'false'.
        /// </remarks>
        protected bool UpdateData(string tableName, string[] columns, string[] values, string condition)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                string updateQuery = $"UPDATE {tableName} SET ";
                for (int i = 0; i < columns.Length; i++)
                {
                    updateQuery += $"{columns[i]}=@{columns[i]}{(i < columns.Length - 1 ? ", " : "")}";

                }
                updateQuery += $" WHERE {condition}";

                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                for (int i = 0; i < columns.Length; i++)
                {
                    command.Parameters.AddWithValue($"@{columns[i]}", values[i]);

                }

                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

        /// <summary>
        /// Deletes data from a specified table based on a condition.
        /// </summary>
        /// <param name="tableName">The name of the table to delete data from.</param>
        /// <param name="condition">The condition used to determine which rows to delete.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        /// <exception cref="MySqlException">Thrown when an error occurs executing the query.</exception>
        /// <remarks>
        /// This method constructs a DELETE query using the specified table name and condition and executes it
        /// using the MySqlConnection object returned by the OpenConnection method. It returns 'true' if the command executes
        /// successfully, otherwise 'false'.
        /// </remarks>
        ///
        ///<example>
        /// This sample demonstrates how to use the DeleteData method.
        ///
        /// <code>
        /// var db = new DatabaseImpl();
        /// bool result = db.DeleteData("myTable", "id = 1");
        /// </code>
        /// </example>
        protected bool DeleteData(string tableName, string condition)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                string deleteQuery = $"DELETE FROM {tableName} WHERE {condition}";
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

    }
}
