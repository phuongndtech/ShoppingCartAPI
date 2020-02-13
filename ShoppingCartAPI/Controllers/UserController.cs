using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.DTO;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserSevice _userServices;
        public UserController(IUserSevice userSevice)
        {
            _userServices = userSevice;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userServices.GetUsers();
        }

        [HttpGet]
        [Route("Id")]
        [Authorize]
        public async Task<User> GetUser(int userId)
        {
            return await _userServices.GetUser(userId);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<string> AddUser([FromBody]UserRequest userRequest)
        {
            return await _userServices.CreateUser(userRequest);
        }

        [HttpPut]
        [Authorize]
        public async Task<string> UpdateUser(int userId, [FromBody]UserRequest userRequest)
        {
            return await _userServices.UpdateUser(userId, userRequest);
        }

        [HttpDelete]
        [Authorize]
        public async Task<string> DeleteUser(int userId)
        {
            return await _userServices.DeteleUser(userId);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userServices.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }
    }
}