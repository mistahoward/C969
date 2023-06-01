using C969.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Data
{
    public class UserData : Database
    {
        /// <summary>
        /// Gets a user from the DB by id
        /// </summary>
        /// <param name="id">ID of the user - userId in db</param>
        /// <returns>User if success, exception if fail</returns>
        /// <exception cref="Exception"></exception>
        public User GetUserById(int id)
        {
            // Create an empty User and null result user
            User emptyUser = new User();
            User resultUser = null;
            // Call retrieve data, read the response and transpose it to the result user
            using (var sqlResponse = RetrieveData(emptyUser, "id", id))
            {
                if (sqlResponse.Read())
                {
                    resultUser = new User
                    {
                        userId = sqlResponse.GetInt32(sqlResponse.GetOrdinal("userId")),
                        userName = sqlResponse.GetString(sqlResponse.GetOrdinal("userName")),
                        password = sqlResponse.GetString(sqlResponse.GetOrdinal("password")),
                        active = sqlResponse.GetBoolean(sqlResponse.GetOrdinal("active")),
                        createDate = sqlResponse.GetDateTime(sqlResponse.GetOrdinal("createDate")),
                        createdBy = sqlResponse.GetString(sqlResponse.GetOrdinal("createdBy")),
                        lastUpdate = sqlResponse.GetDateTime(sqlResponse.GetOrdinal("lastUpdate")),
                        lastUpdateBy = sqlResponse.GetString(sqlResponse.GetOrdinal("lastUpdateBy"))
                    };
                }
            }

            // If result user exists, return it - otherwise, throw an exception
            if (resultUser == null)
            {
                throw new Exception("No user found with the provided ID.");
            }

            return resultUser;
        }
    }

}
