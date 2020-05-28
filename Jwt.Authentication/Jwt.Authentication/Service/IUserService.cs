using Jwt.Authentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Authentication.Service
{
    public interface IUserService
    {
        User Authenticate(string userName, string secret);
        List<User> GetAllUsers();
        User GetUser(string userName);
    }
}
