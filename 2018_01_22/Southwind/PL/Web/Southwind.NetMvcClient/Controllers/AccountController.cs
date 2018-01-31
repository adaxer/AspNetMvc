using Southwind.Interfaces;
using Southwind.NetMvcClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Southwind.NetMvcClient.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationClient _authenticationClient;

        public AccountController(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        public ActionResult Login(string returnUrl)
        {
            var result = View();
            (result as ViewResult).ViewBag.ReturnUrl = returnUrl;
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel login, string returnUrl)
        {
            try
            {
                if (!_authenticationClient.IsLoggedIn)
                    await _authenticationClient.LoginAsync(login.Email, login.Password);
                if (_authenticationClient.IsLoggedIn)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false);
                    return Redirect(returnUrl);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View();
        }
    }
}
