using Southwind.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.CommonLib
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private string bearerToken = "";
        private bool isLoggedIn;
        private LoginData loginData;

        public AuthenticationClient(LoginData data)
        {
            loginData = data;
        }

        public bool IsLoggedIn => isLoggedIn;

        public string HeaderScheme => "Bearer";

        public string AuthenticationValue => bearerToken;

        public async Task LoginAsync(string username = null, string password = null)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "username", username ?? loginData.UserName }, { "password", password ?? loginData.Password } });
                var response = await client.PostAsync(loginData.LoginUrl, content);
                bearerToken = await response.Content.ReadAsStringAsync();
                isLoggedIn = !string.IsNullOrWhiteSpace(bearerToken);
            }
        }
    }
}
