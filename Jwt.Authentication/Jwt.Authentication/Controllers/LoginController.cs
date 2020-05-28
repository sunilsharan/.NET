using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jwt.Authentication.Attributes;
using Jwt.Authentication.Model;
using Jwt.Authentication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Authentication.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserService _UserService;

        public LoginController(IUserService userService)
        {
            _UserService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Authenticate authenticate)
        {
            var user = _UserService.Authenticate(authenticate.Username, authenticate.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            else
                return Ok(user);
        }

        [RoleBasedAuthorization(Roles.Admin)]
        [HttpGet]
        public List<ApiModel.User> Get()
        {
            List<ApiModel.User> result = new List<ApiModel.User>();

            var users =_UserService.GetAllUsers();
            foreach (var item in users)
            {
                result.Add(new ApiModel.User
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Role = item.Role
                });
            }
            return result;
        }

        [RoleBasedAuthorization(Roles.Admin, Roles.User)]
        [HttpGet("GetUser/{id}")]
        public ApiModel.User Get(string id)
        {
            var user= _UserService.GetUser(id);
            return new ApiModel.User { UserId = user.UserId, UserName = user.UserName, Role = user.Role };
        }
    }
}