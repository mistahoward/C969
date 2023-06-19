using C969.Exceptions;
using C969.Models;
using C969.Utilities;
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
        /// Gets a user from the db by id
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>User if success, exception if fail</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when id is less than 0</exception>
        /// <exception cref="DataNotFound">Thrown when user object is not found</exception>
        public User GetUserById(int id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("ID cannot be a number less than zero");
            }
            User emptyUser = new User();
            DataRow userRow = RetrieveSingleRow(emptyUser, "userId", id);
            User resultUser = DataTableConverter.ConvertDataRowToModel<User>(userRow)
                ?? throw new DataNotFound("No user found with the provided ID");
            return resultUser;
        }
    }

}
