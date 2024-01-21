using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Data;
using Data.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Domain.Models;

namespace employee_apis.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private string Secret { get; set; }
        private List<ApplicationUser> _users = new List<ApplicationUser>
        { 
            //new ApplicationUser { FirstName = "Omar", LastName = "Ibrahim", UName = "Omar", Password = "123" },
            //new ApplicationUser { FirstName = "Amin", LastName = "Ibrahim", UName = "Amin", Password = "123" },
            //new ApplicationUser { FirstName = "Gamil", LastName = "Ibrahim", UName = "Gamil", Password = "123" },
            //new ApplicationUser { FirstName = "Khalid", LastName = "Ibrahim", UName = "Khalid", Password = "123" },
            //new ApplicationUser { FirstName = "Saad", LastName = "Ibrahim", UName = "Saad", Password = "123" },
        };

        public UsersController(IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            Secret = configuration["AppSettings:Secretkey"];
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("NormalizedUserName", user.NormalizedUserName),
                    new Claim("UserType", "Standard"),
                    new Claim("UserJob", "Student"),
                    new Claim(ClaimTypes.Role, "Admin"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tok = tokenHandler.WriteToken(token);

            if (tok == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(tok);
        }

        //private async Task<IActionResult> AuthUser(string username, string password)
        //{
        //    var user = await _userManager.FindByNameAsync(username);

        //    // return null if user not found
        //    if (user == null)
        //        return null;

        //    // authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim("NormalizedUserName", user.NormalizedUserName),
        //            //new Claim("LastName", user.LastName),
        //            //new Claim("UName", user.UName)
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    //user.Token = tokenHandler.WriteToken(token);

        //    return tokenHandler.WriteToken(token);
        //}

        public struct userData
        {
            public string AuthorizationAttribute { get; set; }
            public bool IsAuthenticated { get; set; }
            public string Name { get; set; }
            public string NormalizedUserName { get; set; }
            public string UserType { get; set; }
        }
        
        [Authorize]
        [HttpGet("UserData")]
        public IActionResult UserData()
        {
            userData data = new userData();
            data.AuthorizationAttribute = "[Authorize]";
            data.Name = User.Identity.Name;
            data.NormalizedUserName = User.Claims.Where(claim => claim.Type == "NormalizedUserName").ToList()[0].Value;
            data.UserType = User.Claims.Where(claim => claim.Type == "UserType").ToList()[0].Value;
            data.IsAuthenticated = User.Identity.IsAuthenticated;
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("UserDataAdminRole")]
        public IActionResult UserDataAdminRole()
        {
            userData data = new userData();
            data.AuthorizationAttribute = "[Authorize(Roles = 'Admin')]";
            data.Name = User.Identity.Name; 
            data.NormalizedUserName = User.Claims.Where(claim => claim.Type == "NormalizedUserName").ToList()[0].Value;
            data.UserType = User.Claims.Where(claim => claim.Type == "UserType").ToList()[0].Value;
            data.IsAuthenticated = User.Identity.IsAuthenticated;
            return Ok(data);
        }

        [Authorize(Policy = "Student")]
        [HttpGet("UserDataStudentPolicy")]
        public IActionResult UserDataStudentPolicy()
        {
            userData data = new userData();
            data.AuthorizationAttribute = "[Authorize(Policy = 'Student')]";
            data.Name = User.Identity.Name;
            data.NormalizedUserName = User.Claims.Where(claim => claim.Type == "NormalizedUserName").ToList()[0].Value;
            data.UserType = User.Claims.Where(claim => claim.Type == "UserType").ToList()[0].Value;
            data.IsAuthenticated = User.Identity.IsAuthenticated;
            return Ok(data);
        }


        [AllowAnonymous]
        [HttpGet("faceback")]
        public async System.Threading.Tasks.Task<ActionResult<JsonDocument>> faceback()
        {
            var client = new HttpClient();
            string url = @"https://graph.facebook.com/oauth/access_token?client_id=3168317616542352&redirect_uri=https://localhost:44391/users/faceback&client_secret=3484f06f53d601803a9287099ecf66ab&code=";
            url += Request.Query["code"][0];
            var response = await client.GetStringAsync(url);
            var details = JObject.Parse(response);

            url = @"https://graph.facebook.com/v5.0/me?fields=id,email,gender,link,locale,last_name,first_name,middle_name,timezone,updated_time,verified&access_token=";
            url += details["access_token"].ToString();
            response = await client.GetStringAsync(url);
            JsonDocument document = JsonDocument.Parse(response);
            return document;
        }

        [AllowAnonymous]
        [HttpGet("logwithfacebook")]
        public void logwithfacebook()
        {
            var client = new HttpClient();
            string url = @"https://www.facebook.com/v5.0/dialog/oauth?client_id=3168317616542352&redirect_uri=https://localhost:44391/users/faceback&state={st=Tralamlam123456789,ds=123456789}";
            var response = client.GetAsync(url);
            //Response.Redirect("https://localhost:44391/users/faceback");
            //return Ok(response);
        }
                

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = GetAllAppUsers();
            return Ok(users);
        }

        private IEnumerable<ApplicationUser> GetAllAppUsers()
        {
            return _users.WithoutPasswords();
        }
    }
}
