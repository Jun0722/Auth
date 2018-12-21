using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        public IActionResult test()
        {
            return Ok();
        }


        [HttpPost]
        public JsonResult GetToken(UserModel model)
        {
            var user = new UserModel()
            {
                Id = 1,
                Email = "admin@test.com",
                Name = "admin",
                Password = "admin"
            };
            if(user==null)
            {   }

            var tokenHanlder = new JwtSecurityTokenHandler();
            //keep one
            var key= Encoding.ASCII.GetBytes("this is a security key");

            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"https://localhost:5001"),

                    new Claim(JwtClaimTypes.Id,user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name,user.Name)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            var tokenString = tokenHanlder.WriteToken(token);
            var data = new
            {
                accss_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = user.Id,
                    name = user.Name,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()

                }
            };
            return Json(data);
        }
    }
}
