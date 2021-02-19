using EdDSAJwtBearer;
using EdDSAJwtBearerSSOServer.Models;
using EdDSAJwtBearerSSOServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
namespace EdDSAJwtBearerSSOServer.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        EdDSAJwtBearServer Server;
        public AccountController(EdDSAJwtBearServer server)
        {
            Server = server;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]UserCredentials credentials)
        {
            IActionResult Response = Unauthorized();
            var User = Repository.GetUser(credentials.Email, credentials.Password);
            if (User != null)
            {
                string Token = CreateToken(Server, User);
                Response = Ok(Token);
            }
            return Response;
        }
        private string CreateToken(EdDSAJwtBearServer server, User user)
        {
            var Claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("email", user.Email)
            };
            return Server.CreateToken(Claims, user.Roles, DateTime.Now.AddMinutes(30));
        }
    }
}
