using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using crudLoginReact.Models;
using crudLoginReact.Controllers;

namespace crudLoginReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretKey;
        private readonly UserContext userContext;

        public AuthenticationController(IConfiguration config, UserContext userContext)
        {
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            this.userContext = userContext;
        }


        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario request)
        {
            var usuario = userContext.Usuario
                .Where(x => x.id_user == request.id_user && x.contrasena == request.contrasena)
                .FirstOrDefault();            
            //Parse request.id_user to string and save it in a variable
            var id_user = usuario.id_user.ToString();
            Console.WriteLine(id_user);
            try
            {
                if (usuario != null)
                {
                    var keyBytesw = Encoding.ASCII.GetBytes(secretKey);
                    var claims = new ClaimsIdentity();
                    //Parse request.id_user to string and save it in a variable
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.id_user.ToString()));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytesw), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string token = tokenHandler.WriteToken(tokenConfig);
                    Console.WriteLine(token);
                    return StatusCode(StatusCodes.Status200OK, new { token = token });
                }
                else
                {
                    //return the error message
                    return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuario o contraseña incorrectos" });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }

            

        }
    }
}
