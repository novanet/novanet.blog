using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.BankId.Models;
using Blog.BankId.Signicat;
using Microsoft.AspNetCore.Mvc;

namespace Blog.BankId.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISignicatApi _signicatApi;

        public HomeController(ISignicatApi signicatApi)
        {
            _signicatApi = signicatApi;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("redirect")]
        public async Task<ActionResult> BankIdCallback(string code, string state)
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("demo-preprod:mqZ-_75-f2wNsiQTONb7On4aAZ7zc218mrRVk1oufa8"));

            var token = await _signicatApi.GetToken(
                $"Basic {credentials}",
                new Dictionary<string, object>
                {
            {"client_id", "demo-preprod"},
            {"redirect_uri", "https://localhost:5000/redirect"},
            {"grant_type", "authorization_code"},
            {"code", code}
                });

            var userInfo = await _signicatApi.GetUserInfo($"Bearer {token.AccessToken}");

            return View("BankIdCallBack", userInfo.SocialSecurityNumber);
        }
    }
}
