using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TitanGate.Website.Api.Contracts;
using TitanGate.Website.Api.Domain.Settings;
using TitanGate.Website.Api.Filters;
using TitanGate.Website.Api.Handlers.Contracts;

namespace TitanGate.Website.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private ClientApiSettings _settings;
        private readonly IClientCreateHandler _createHandler;
        private readonly IClientLoginHandler _loginHandler;

        public ClientsController(IOptions<ClientApiSettings> settings, 
                                IClientCreateHandler createHandler, 
                                IClientLoginHandler loginHandler)
        {
            _settings = settings.Value;
            _createHandler = createHandler;
            _loginHandler = loginHandler;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody]ClientRequest login)
        {
            var isAuthenticated = await _loginHandler.HandleLoginRequest(login);

            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            var tokenString = GenerateJSONWebToken(login);

            return Ok(new { token = tokenString });
        }

        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(IPFilterAccess))]
        public async Task<IActionResult> CreateClient([FromBody]ClientRequest command)
        {
            await _createHandler.HandleCreateRequest(command);
            return Ok();
        }

        private string GenerateJSONWebToken(ClientRequest userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Website, userInfo.ClientId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
              issuer: _settings.JwtIssuer,
              audience: _settings.JwtIssuer,
              claims: claims,
              expires: DateTime.Now.AddMinutes(double.Parse(_settings.JwtExpirationInMinutes)),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
