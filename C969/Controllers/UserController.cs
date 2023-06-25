using C969.Data;
using C969.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Controllers
{
    public class UserController
    {
        private readonly UserData _userData;
        public UserController() {
            _userData = new UserData();
        }
        /// <summary>
        /// Tries to login a user with the given credentials
        /// </summary>
        /// <param name="username">The username to use</param>
        /// <param name="password">The password to use</param>
        /// <returns>Returns true if the user is logged in, otherwise false.</returns>
        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                User claimedUser = _userData.GetUserByName(username);
                if (claimedUser.password == password)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}
