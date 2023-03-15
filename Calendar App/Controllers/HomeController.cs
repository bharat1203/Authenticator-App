using Calendar_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        string credentialsFile = "C:\\Users\\BharatKumarPamarthi\\Documents\\Bharat Desktop\\Calendar App\\Calendar App\\credentials.json";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var redirectUrl = "https://login.microsoftonline.com/organizations/oauth2/v2.0/authorize?" +
                             "&scope=" + credentials["scopes"].ToString() +
                             "&response_type =code" +
                             "&response_mode = query" +
                             "&state=12345" +
                             "&redirect_uri=" + credentials["redirect_url"].ToString() +
                             "&client_id=" + credentials["client_id"].ToString();




            return Redirect(redirectUrl);
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
    }
}
