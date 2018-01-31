using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.Interfaces
{
    public interface IAuthenticationClient
    {
        bool IsLoggedIn { get; }

        string HeaderScheme { get; }

        string AuthenticationValue { get; }

        Task LoginAsync(string username=null, string password=null);
    }

    public struct LoginData
    {
        public string LoginUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
