using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.ApiResponses
{
    public class ApiErrorMessage
    {
        public const int GENERIC_ERROR_CODE = 9999;

        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public static ApiErrorMessage FromException(Exception exception)
        {
            return new ApiErrorMessage() { ErrorMessage = exception.Message, ErrorCode = GENERIC_ERROR_CODE };
        }
    }
}
