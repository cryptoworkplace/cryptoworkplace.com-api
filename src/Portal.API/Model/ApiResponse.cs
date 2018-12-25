using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Portal.API.Model
{
    public class ApiResponse
    {
        public static ApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new ApiResponse(statusCode, result, errorMessage);
        }

        public int StatusCode { get; set; }
        public string RequestId { get; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        protected ApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
