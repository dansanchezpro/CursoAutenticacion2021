using BlazorClient.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorClient.Pages
{
    public partial class Index : ComponentBase
    {
        const string SSOUrl = "https://localhost:44330/";
        const string ResourceWebApi = "https://localhost:44341/";

        string FullName;
        string[] Roles;
        string Token;
        string InformationMessage;
        UserCredentials UserCredentials = new UserCredentials();

        public async Task<(string ErrorMessage, string Token)>
            GetTokenAsync(UserCredentials userCredentials)
        {
            StringContent Content =
                new StringContent(JsonSerializer.Serialize(userCredentials),
                Encoding.UTF8, "application/json");

            (string ErrorMessage, string Token) Result = (null, null);
            HttpClient HttpClient = new HttpClient();
            var Response = await HttpClient.PostAsync($"{SSOUrl}login", Content);
            if (Response.IsSuccessStatusCode)
            {
                Result.Token = await Response.Content.ReadAsStringAsync();
            }
            else
            {
                Result.Token = null;
                Result.ErrorMessage = Response.ReasonPhrase;
            }
            return Result;
        }
        public async Task<string> GetDataAsync(string url, string token = null)
        {
            var HttpClient = new HttpClient();
            HttpResponseMessage Response;
            string ResultData = "";
            if (token != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
            Response = await HttpClient.GetAsync(url);
            if (Response.IsSuccessStatusCode)
            {
                string Result = await Response.Content.ReadAsStringAsync();
                ResultData = Result;
            }
            else
            {
                ResultData = Response.ReasonPhrase;
            }
            return ResultData;
        }
        public string GetClaimValue(string token, string claimType)
        {
            var Handler = new JwtSecurityTokenHandler();
            var Token = Handler.ReadJwtToken(token);
            return Token.Claims.Where(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Value).FirstOrDefault();
        }
        public string[] GetRoles(string token)
        {
            var Handler = new JwtSecurityTokenHandler();
            var Token = Handler.ReadJwtToken(token);

            var Roles = Token.Claims.Where(c => c.Type.Equals("role", StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Value).ToArray();
            return Roles;
        }
    }
}
