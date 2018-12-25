using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.API
{
    public static class ResponseWrapperMiddleware
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }
}
