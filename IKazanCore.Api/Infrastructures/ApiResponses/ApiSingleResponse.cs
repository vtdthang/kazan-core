using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.ApiResponses
{
    public class ApiSingleResponse<T> : ApiResponseBase
    {
        #region Constructors

        public ApiSingleResponse()
        {
            //base.Errors = new List<ApiErrorMessage>();
        }

        public ApiSingleResponse(T value)
        {
            this.Data = value;
            //base.Errors = new List<ApiErrorMessage>();
        }

        public ApiSingleResponse(T value, IList<ApiErrorMessage> errors) : base(errors)
        {
            this.Data = value;
            //base.Errors = errors;
        }

        #endregion

        #region Properties
        public T Data { get; set; }

        #endregion
    }
}
