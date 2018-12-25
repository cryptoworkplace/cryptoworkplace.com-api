using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Portal.API.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Portal.API
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //reset the body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                var objResult = JsonConvert.DeserializeObject(readToEnd);
                var result = ApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, null);
                var (formatter, formatterContext) = context.SelectFormatter(result);

                await formatter.WriteAsync(formatterContext);

                //await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}
