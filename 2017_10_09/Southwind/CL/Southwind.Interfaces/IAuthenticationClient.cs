using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Southwind.Contracts.Interfaces
{
    public interface IAuthenticationClient
    {
        bool IsLoggedIn { get; }

        string HeaderScheme { get; }

        string AuthenticationValue { get; }

        Task LoginAsync();
    }

    public struct LoginData
    {
        public string LoginUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}