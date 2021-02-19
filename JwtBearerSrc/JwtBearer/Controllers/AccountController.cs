using JwtBearerA.Models;
using JwtBearerA.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtBearerA.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        IConfiguration Configuration;
        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserCredentials userCredentials)
        {
            IActionResult Response = Unauthorized();
            var User = Repository.GetUser(userCredentials.Email, userCredentials.Password);
            if (User != null)
            {
                string Token = CreateToken(User);
                Response = Ok(Token);
            }
            return Response;
        }
        private string CreateToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Convert.FromBase64String(Configuration["JWT:SecretKey"]);

            var Claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("email", user.Email)
            };
            if (user.Roles != null)
            {
                foreach (var Role in user.Roles)
                {
                    Claims.Add(new Claim("role", Role));
                }
            }
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key)
                , SecurityAlgorithms.HmacSha256Signature)
            };

            int? ExpiresValue = Configuration.GetValue<int?>("JWT:ExpiresMinutes");
            string IssuerValue = Configuration.GetValue<string>("JWT:ValidIssuer");
            string AudienceValue = Configuration.GetValue<string>("JWT:ValidAudience");
            
            if (ExpiresValue.HasValue) TokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(ExpiresValue.Value);
            if (IssuerValue != null) TokenDescriptor.Issuer = IssuerValue;
            if (AudienceValue != null) TokenDescriptor.Audience = AudienceValue;

            SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(Token);
        }

    }
}
