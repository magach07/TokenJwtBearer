using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenJwtBearer.Model;
using TokenJwtBearer.Repository;
using TokenJwtBearer.Services;

namespace TokenJwtBearer.Controllers
{
    [Route("v1/TestingUserValidation")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate ([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return NotFound("Usuário ou senha inválidos.");
            }

            var token = TokenService.GenerateToken(model);
            user.Password = "";

            return new 
            {
                user = user,
                token = token
            };
        }
        [HttpGet]
        [Route("Anônimo")]
        [AllowAnonymous]
        public string Anonymous () => "Anônimo";

        [HttpGet]
        [Route("Authorized")]
        [Authorize]
        public string Authorized () => String.Format("Usuário autorizado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("Manager")]
        [Authorize(Roles = "manager")]
        public string Manager () => String.Format("Usuário Manager autorizado - {0}", User.Identity.Name);
    }
}