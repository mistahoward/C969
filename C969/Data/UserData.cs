using C969.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            DataTable dt = RetrieveData(emptyUser, "userId", id);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var userId = row.Field<int>("userId");
                var userName = row.Field<string>("userName");
                var password = row.Field<string>("password");
                var active = Convert.ToBoolean(row["active"]);
                var createDate = row.Field<DateTime>("createDate");
                var createdBy = row.Field<string>("createdBy");
                var lastUpdate = row.Field<DateTime>("lastUpdate");
                var lastUpdateBy = row.Field<string>("lastUpdateBy");

                resultUser = new User
                {
                    userId = userId,
                    userName = userName,
                    password = password,
                    active = active,
                    createDate = createDate,
                    createdBy = createdBy,
                    lastUpdate = lastUpdate,
                    lastUpdateBy = lastUpdateBy
                };
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
