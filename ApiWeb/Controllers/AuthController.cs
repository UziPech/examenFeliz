using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApiWeb.Models;
using ApiWeb.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ApiWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Usuario fijo para el ejemplo
        if (request.Username == "admin" && request.Password == "1234")
        {
            var token = JwtHelper.GenerateToken(request.Username, "admin", _config);
            return Ok(new { token });
        }

        return Unauthorized(new { message = "Credenciales inválidas." });
    }
}
