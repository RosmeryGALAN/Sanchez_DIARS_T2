using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Pokemon.Extensions;
using System.Threading.Tasks;
using AppCore.BD;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Pokemon.Controllers
{

    public class AuthController : Controller
    {
        SimuladorContext context = new SimuladorContext();
        private IConfiguration configuration;
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(String UserName, String pass)
        {
            var userv = context.Entrenadores.FirstOrDefault(o => o.UserName == UserName && o.Pass == pass);

            if (userv == null)
                return View();


            HttpContext.Session.Set("sessionUser", userv);
            ViewBag.User = HttpContext.Session.GetString("sessionUser");

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, userv.UserName),
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(userIdentity);

            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Pokemon", new { id = userv.Id });
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "AUth");
        }

        public String GetBashPassword(String pass)
        {
            string token = configuration.GetValue<string>("Token");
            pass = pass + token;
            var sha = SHA256.Create();
            var hashdata = sha.ComputeHash(Encoding.Default.GetBytes(pass));
            return Convert.ToBase64String(hashdata);
        }
    }
}
