using Jwt.Authentication.Attributes;
using Jwt.Authentication.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Authentication.Service
{
    public class UserService : IUserService
    {
        List<User> _Users = new List<User>();

        // private  string SecretKey = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";
        AppSettings _Settings;
        public UserService(IOptions<AppSettings> appSetting)
        {
            _Settings = appSetting.Value;
            _Users.Add(new User
            {
                UserName = "Mark",
                Secret = "abc@123",
                UserId = 1,
                Role = new Role { RoleId = 1, RoleName = Roles.Admin }
            });
            _Users.Add(new User
            {
                UserName = "Adam",
                Secret = "abc@123",
                UserId = 1,
                Role = new Role { RoleId = 1, RoleName = Roles.Admin }
            });
            _Users.Add(new User
            {
                UserName = "John",
                Secret = "abc@123",
                UserId = 1,
                Role = new Role { RoleId = 1, RoleName = Roles.User }
            });
        }
        public User Authenticate(string userName, string secret)
        {
            User user = _Users.SingleOrDefault<User>(o => o.UserName.ToLower() == userName.ToLower() && o.Secret == secret);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_Settings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role.RoleName)
                    }
                    ),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _Users;
        }
        public User GetUser(string userName)
        {
            return _Users.SingleOrDefault<User>(o => o.UserName.ToLower() == userName.ToLower());
        }
    }
}
