using Calltech.Models;
using Calltech.Models.Common;
using Calltech.Models.Request;
using Calltech.Models.Response;
using Calltech.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Calltech.Services
{
    public class Userservices : IUserService
    {
        private readonly AppSettings _appSetting;

        public Userservices(IOptions<AppSettings> appSettings)
        {

            _appSetting = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            using (var db = new TestContext())
            {
                string spassword = Encrypt.GetSHA256(model.Password);
                var usuario = db.Usuarios.Where(d => d.Login == model.Login &&
                                               d.Password == spassword).FirstOrDefault();

                if (usuario == null) return null;

                userResponse.Login = usuario.Login;
                userResponse.Token = GetToken(usuario);
            }

            return userResponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSetting.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                     new Claim[]
                     {
                         new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                         
                     }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
