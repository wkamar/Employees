using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Data;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace employee_apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public HomeController()
        //{
        //}

        //[HttpPost]
        //[Route("login")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthModel userData)
        {
            //login functionality
            var user = await _userManager.FindByNameAsync(userData.Username);

            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, userData.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok("User has been Logged in");
                }
            }

            return Ok("Login failed");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthModel userData)
        {
            var student = new Student()
            {
                FirstName = userData.Username,
                LastName = userData.Username
            };

        var user = new ApplicationUser
        {
            UserName = userData.Username,
            Email = "",
            Student = student
        };
            
            var result = await _userManager.CreateAsync(user, userData.Password);

            if (result.Succeeded)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, userData.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok("User has been registered successfully");
                }
            }

            return Ok("Register failed");
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok("User has been Logged out");
        }
    }


}