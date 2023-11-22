using HDIApi.Bussines.Interface;
using HDIApi.DTOs;
using HDIApi.Models;
using HDIApi.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUsersProvider _usersProvider;

        public UserController(IUsersProvider usersProvider, ILogger<UserController> logger)
        {
            _logger = logger;
            _usersProvider = usersProvider;
        }

        [HttpPost("LoginEmployee")]
        public async Task<IActionResult> LoginEmployee([FromBody] LoginDTO infologin)
        {
            IActionResult result;
            try
            {
                Employee userInfo = await _usersProvider.LoginEmployee(infologin);
                if (userInfo == null || !userInfo.Password.Equals(infologin.Password))
                {
                    result = BadRequest();
                }
                else
                {

                        var token = new TokenDTO
                        {
                            token = "",
                            idUser = userInfo.IdEmployee,
                            role = userInfo.Rol,
                            fullName = userInfo.NameEmployee + " " + userInfo.LastnameEmployee
                        };
                    result = Ok(TokenGenerator.GetToken(token));


                }
            }
            catch (Exception)
            {
                result = StatusCode(500);
            }
            return result;
        }
    }
}
