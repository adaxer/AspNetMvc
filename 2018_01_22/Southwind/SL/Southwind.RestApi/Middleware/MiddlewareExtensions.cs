using Southwind.RestApi.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCultureAdaption(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdaptCulture>();
        }

        public static IApplicationBuilder UseIdentityAdaption(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdaptIdentity>();
        }
    }
}
