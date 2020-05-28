using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Jwt.Authentication.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Secret { get; set; }

        public Role  Role { get; set; }

        public string Token { get; set; }
    }
}
