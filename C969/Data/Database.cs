using C969.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;

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
        /// Retrieves data of a model based on provided conditions
        /// </summary>
        /// <typeparam name="T">the type of the model</typeparam>
        /// <param name="model">Model object</param>
        /// <param name="conditionColumn">Column name</param>
        /// <param name="conditionValue">Value to match</param>
        /// <returns>DataTable or false if fail</returns>
        /// <exception cref="Exception"></exception>
        protected DataTable RetrieveData<T>(T model, string conditionColumn, object conditionValue) where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                // Getting the table name from the type of model, lower casing it to match db ERD
                string tableName = (typeof(T).Name).ToLower();

                // Extract property names and values from the model
                PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                List<string> columnNames = new List<string>();

                foreach (PropertyInfo prop in properties)
                {
                    string propName = prop.Name;
                    columnNames.Add(propName);
                }

                // Construct the SQL query
                string columns = string.Join(", ", columnNames);
                string query = $"SELECT {columns} FROM {tableName} WHERE {conditionColumn} = @value";
                MySqlCommand command = new MySqlCommand(query, connection);

                // Add value as a parameter to prevent SQL Injection
                command.Parameters.AddWithValue("@value", conditionValue);

                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }
        /// <summary>
        /// Retrieves data from the database for a specified model using a condition column and a start value
        /// and end value range.
        /// </summary>
        /// <typeparam name="T">The data model type</typeparam>
        /// <param name="model">The model instance used for retrieving dat.</param>
        /// <param name="conditionColumn">The column name used as a condition for retrieving data</param>
        /// <param name="startValue">The start value of the range to retrieve data</param>
        /// <param name="endValue">The end value of the range to retrieve data</param>
        /// <returns>A DataTable instance containing the retrieved data</returns>
        protected DataTable RetrieveData<T>(T model, string conditionColumn, object startValue, object endValue) where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                string tableName = (typeof(T).Name).ToLower();
                PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                List<string> columnNames = new List<string>();

                foreach (PropertyInfo prop in properties)
                {
                    string propName = prop.Name;
                    columnNames.Add(propName);
                }

                string columns = string.Join(", ", columnNames);
                string query = $"SELECT {columns} FROM {tableName} WHERE {conditionColumn} BETWEEN @startValue AND @endValue";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@startValue", startValue);
                command.Parameters.AddWithValue("@endValue", endValue);

                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

        /// <summary>
        /// Retrieves all data of a model type (overload method)
        /// </summary>
        /// <typeparam name="T">the type of the model</typeparam>
        /// <returns>DataTable or false if fail</returns>
        /// <exception cref="Exception"></exception>
        protected DataTable RetrieveData<T>() where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                // Getting the table name from the type of model, lower casing it to match db ERD
                string tableName = (typeof(T).Name).ToLower();

                // Construct the SQL query
                string query = $"SELECT * FROM {tableName}";
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("An error occurred while executing the query", ex);
                }
            }
        }

        /// <summary>
        /// Retrieves a single row from the database that matches the specified condition
        /// </summary>
        /// <typeparam name="T">The data model type</typeparam>
        /// <param name="model">The instance of the model used to retrieve the data</param>
        /// <param name="conditionColumn">The name of the column to match the value against</param>
        /// <param name="conditionValue">The value to match the column with.</param>
        /// <returns>A single DataRow that matches the specified condition - or null if not found</returns>
        protected DataRow RetrieveSingleRow<T>(T model, string conditionColumn, object conditionValue) where T : class
        {
            // Calls RetrieveData method
            DataTable dt = RetrieveData(model, conditionColumn, conditionValue);

            // Check if any rows were returned
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            // Return the first row
            return dt.Rows[0];
        }

        /// <Summary>
        /// Adds data to model
        /// </Summary>
        /// <typeparam name="T">The type of the model</typeparam>
        /// <param name="model">Model object with the data to add</param>
        /// <returns>True if the data was successfully added, false otherwise.</returns>
        protected bool AddData<T>(T model) where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                // Getting the table name from the type of model, lower casing it to match db ERD
                string tableName = (typeof(T).Name).ToLower();

                // Extract property names and values from the model
                PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                List<string> columnNames = new List<string>();
                List<string> paramNames = new List<string>();
                foreach (PropertyInfo prop in properties)
                {
                    string propName = prop.Name;
                    columnNames.Add(propName);
                    paramNames.Add("@" + propName);
                }

                // Construct the SQL query
                string columns = string.Join(", ", columnNames);
                string values = string.Join(", ", paramNames);
                string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                // Add parameters to the command
                foreach (PropertyInfo prop in properties)
                {
                    string paramName = "@" + prop.Name;
                    object paramValue = prop.GetValue(model);
                    command.Parameters.AddWithValue(paramName, paramValue);
                }

                // Execute Query
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
        /// Updates data in DB
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="model">Model Object (table name)</param>
        /// <param name="identifierName">Column Name in DB</param>
        /// <param name="identifierValue">Matched value of said object </param>
        /// <returns>True if the data was successfully added, false otherwise.</returns>
        /// <exception cref="Exception"></exception>
        protected bool UpdateData<T>(T model, string identifierName, object identifierValue) where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                // Getting the table name from the type of model, lower casing it to match db ERD
                string tableName = (typeof(T).Name).ToLower();

                // Extract property names and values from the model
                PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                List<string> setPairs = new List<string>();
                // Extract extract key and value from column
                foreach (PropertyInfo prop in properties)
                {
                    string propName = prop.Name;
                    setPairs.Add($"{propName} = @{propName}");
                }

                // Combine set pairs
                string setPairsString = string.Join(", ", setPairs);

                // Construct query
                string updateQuery = $"UPDATE {tableName} SET {setPairsString} WHERE {identifierName} = @identifierValue";

                // Open connection
                MySqlCommand command = new MySqlCommand(updateQuery, connection);

                // Loop through properties, adding and binding parameter, matched with the model of T
                foreach (PropertyInfo prop in properties)
                {
                    command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(model));
                }
                command.Parameters.AddWithValue("@identifierValue", identifierValue);

                // Execute query
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
        /// Deletes data in DB
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="condition">Condition to search on - example "id = 0".</param>
        /// <returns>True if the data was successfully deleted, false otherwise.</returns>
        /// <exception cref="Exception"></exception>
        protected bool DeleteData<T>(string condition) where T : class
        {
            using (MySqlConnection connection = OpenConnection())
            {
                // Getting the table name from the type of model, lower casing it to match db ERD
                string tableName = (typeof(T).Name).ToLower();

                // Constructing Query
                string deleteQuery = $"DELETE FROM {tableName} WHERE {condition}";

                // Opening connection
                MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                // Execute query
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

