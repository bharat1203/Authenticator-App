using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Calendar_App.Controllers
{
    public class OauthController : Controller
    {
        string credentialsFile = "C:\\Users\\BharatKumarPamarthi\\Documents\\Bharat Desktop\\Calendar App\\Calendar App\\credentials.json";
        string tokensfile = "C:\\Users\\BharatKumarPamarthi\\Documents\\Bharat Desktop\\Calendar App\\Calendar App\\tokens.json";
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Callback(string code, string state, string error)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            if (!string.IsNullOrWhiteSpace(code))
            {
                //RestClient restClient = new RestClient();
                RestRequest restRequest = new RestRequest();

                restRequest.AddParameter("client_id", credentials["client_id"].ToString());
                restRequest.AddParameter("scope", credentials["scopes"].ToString());
                restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
                restRequest.AddParameter("code", code);
                restRequest.AddParameter("grant_type", "authorization_code");
                restRequest.AddParameter("client_secret",credentials["client_secret"].ToString());

                var httpClient = new HttpClient{ BaseAddress = new Uri("https://login.microsoftonline.com/organizations/oauth2/v2.0/token") };
                RestClient client = new RestClient(httpClient, new RestClientOptions(httpClient.BaseAddress));
               

                var response = client.Post(restRequest);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensfile, response.Content);
                    return RedirectToAction("Index", "Home");
                }



            }
            return RedirectToAction("Error");

        }
    }
}
