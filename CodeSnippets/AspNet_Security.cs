///////////////////////////////////
// Security per Custom Middleware

// In Startup:
	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		//...
		app.UseMiddleware<SimpleAuthentication>();
		//...
	}

// Middleware
public class SimpleAuthentication
{
	private readonly RequestDelegate _next;

	public SimpleAuthentication(RequestDelegate next){_next = next;}

	public async Task Invoke(HttpContext context)
 	{
		string authHeader = context.Request.Headers["Authorization"];
		if (authHeader != null && authHeader.StartsWith("Basic"))
		{
			//Extract credentials
			string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
			Encoding encoding = Encoding.GetEncoding("iso-8859-1");
			string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

			int seperatorIndex = usernamePassword.IndexOf(':');

			var username = usernamePassword.Substring(0, seperatorIndex);
			var password = usernamePassword.Substring(seperatorIndex + 1);

			if (username == "test" && password == "test")
			{
				context.Request.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(new GenericIdentity("Vollchecker", "Allesdürfer"));
				await _next.Invoke(context);
				return;
			}
			// no authorization header
			context.Response.StatusCode = 401; //Unauthorized
		}
	}
}

/////////////////////////////////////////////////////////
// Security per Authorize und Cookies

// Startup
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		  .AddCookie(options => options.LoginPath = "/Home/Login"});
		// ...
	}
	
	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		// ...
		app.UseAuthentication();
		// ...
	}

// Logindata
public class LoginData
{
	public string Name { get; set; }
	public string Password { get; set; }
}

	
// Controller
public class HomeController : Controller
{
	[Authorize]
	public IActionResult Secret()
	{
		ViewData["Message"] = "You are logged in to see that secret";
		return View();
	}

	[AllowAnonymous]
	[HttpGet]
	public IActionResult Login(string returnUrl)
	{
		ViewData["ReturnUrl"] = returnUrl;
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginData loginData, string returnUrl)
	{
		await SignIn(loginData);
		return Redirect(returnUrl);
	}

	private async Task SignIn(LoginData loginData)
	{
		ClaimsIdentity identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, loginData.Name) }, CookieAuthenticationDefaults.AuthenticationScheme);
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);

		await HttpContext.SignInAsync(
		  CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = false }
		);
	}
}
//////////////////////////////////////////////////////////

	


