using Calltech.Models.Request;
using Calltech.Models.Response;
using Calltech.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            
            Respuesta respuesta = new Respuesta();

            var useresponse = _userService.Auth(model);

            if (useresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }

            respuesta.Exito = 1;
            respuesta.Data = useresponse;

            return Ok(respuesta);
        
    }
    }
}
