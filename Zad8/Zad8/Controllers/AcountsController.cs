using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zad8.Models;
using Zad8.Services;

namespace Zad8.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AcountsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public AcountsController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var claims = new Claim[]
            {
                new (ClaimTypes.Name, "jan123"),
                new ("Custom", "SomeData"),
                new Claim(ClaimTypes.Role, "admin")
            };

            var secret = "dbs.utyhrrteh.qwewegrre.345ythrhb.fsfvdfbdfb.asdafweff.5y46uhgffbdffgdgrthtkuioyu.fwegrethtyj,rthtr.gwe/wrgehrtheq.wgtyikytujhwg";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var options = new JwtSecurityToken("http://localhost:5000", "http://localhost:5000",
                claims, expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds);

            var refreshToken = "";
            using(var genNum = RandomNumberGenerator.Create())
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            await _dbService.SendLoginData(request);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(options),
                refreshToken
            });
        }
    }
}
