using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth_Cookie.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Auth_Cookie.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth_Cookie.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthDbContext _context;

        public HomeController(AuthDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (loginuser == null)
                return BadRequest("没有该用户");
            if (loginuser.Password != user.Password)
                return BadRequest("密码错误");

            //声明对象创建
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
            //写入HttpContext

            return RedirectToAction("Index", "Test");
        }

    }
}
