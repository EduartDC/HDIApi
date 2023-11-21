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
            try
            {
                Employee userInfo = await _usersProvider.LoginEmployee(infologin);
                if (userInfo == null)
                {
                    return BadRequest(new { message = "Usuario o Contraseña incorrecta." });
                }
                else
                {
                    Console.WriteLine("firstElse");
                    if (userInfo.Password.Equals(infologin.Password))
                    {
                        Console.WriteLine("secondIf");

                        var token = new TokenDTO
                        {
                            Token = "",
                            idUser = userInfo.IdEmployee,
                            Role = userInfo.Rol,
                            FullName = userInfo.NameEmployee + " " + userInfo.LastnameEmployee
                        };
                        return Ok(TokenGenerator.GetToken(token));
                    }
                    else
                    {
                        return BadRequest(new { message = "Usuario o Contraseña incorrecta." });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
