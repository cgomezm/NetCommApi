using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCommApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        JwtOptions JwtOptions;
        public TokenController(JwtOptions jwtOptions)
        {
            this.JwtOptions = jwtOptions;
        }

        [HttpPost]
        public IActionResult GenerateToken(User user)
        {
            if (!new string[] { "user", "admin", "baby" }.Contains(user.Username) || user.Password != "pass")
                return Unauthorized();


            var authClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, user.Username == "admin" ? "admin" : "user"),
                new Claim(ClaimTypes.DateOfBirth, user.Username == "baby" ? "07/07/2005" : "07/07/1994")
            };

            if (user.Username != "baby")
                authClaim.Add(new Claim("Citizenship", "Colombian"));

            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: authClaim,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                    new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecureKey)), SecurityAlgorithms.HmacSha256Signature));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiresIn = 5
                });
        }
    }
}
