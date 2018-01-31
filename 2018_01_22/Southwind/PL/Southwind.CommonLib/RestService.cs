using Newtonsoft.Json;
using Southwind.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.CommonLib
{
    public class RestService : IRestService
    {
        private IAuthenticationClient authenticationClient;

        public RestService(IAuthenticationClient authClient)
        {
            authenticationClient = authClient;
        }

        public async Task<T> GetDataAsync<T>(string url)
        {
            try
            {
                if (!authenticationClient.IsLoggedIn)
                    await authenticationClient.LoginAsync();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authenticationClient.HeaderScheme, authenticationClient.AuthenticationValue);
                    client.DefaultRequestHeaders.Add("TargetLanguage", "fr-FR");
                    var json = await client.GetStringAsync(url);
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PostDataAsync<T>(T value, string url)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(value);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
            }
        }
    }
}
