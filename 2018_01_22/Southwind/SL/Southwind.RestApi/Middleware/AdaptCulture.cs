using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Southwind.RestApi.Middleware
{
    public class AdaptCulture
    {
        private RequestDelegate _next;

        public AdaptCulture(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue("TargetLanguage", out StringValues culture);
            if(culture.Count!=0)
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture.First());
            await _next(context);
        }
    }
}
