using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt.Authentication.Attributes
{
    public class RoleBasedAuthorizationAttribute: AuthorizeAttribute
    {
        public RoleBasedAuthorizationAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
