using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace employee_apis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacebookloginController : ControllerBase
    {
        [HttpGet]
        public void logwithfacebook()
        {
            var client = new HttpClient();
            string url = @"https://www.facebook.com/v5.0/dialog/oauth?client_id=3168317616542352&redirect_uri=https://localhost:44391/users/faceback&state={st=Tralamlam123456789,ds=123456789}";
            var response = client.GetAsync(url);
            //Response.Redirect("https://localhost:44391/users/faceback");
            //return Ok(response);
        }
    }
}