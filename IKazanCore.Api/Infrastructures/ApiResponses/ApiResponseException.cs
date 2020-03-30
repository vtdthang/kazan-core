using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.ApiResponses
{
    public class ApiResponseException : System.Exception
    {
        #region ErrorMessages
        public IList<ApiErrorMessage> ErrorMessages { get; set; }
        #endregion

        #region StatusCode
        public HttpStatusCode StatusCode { get; set; }
        #endregion
    }
}
