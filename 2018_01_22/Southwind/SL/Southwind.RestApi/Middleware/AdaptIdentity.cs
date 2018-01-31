using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Southwind.RestApi.Middleware
{
    public class AdaptIdentity
    {
        private RequestDelegate _next;

        public AdaptIdentity(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Thread.CurrentPrincipal = context.Request.HttpContext.User;
            await _next(context);
        }
    }
}
