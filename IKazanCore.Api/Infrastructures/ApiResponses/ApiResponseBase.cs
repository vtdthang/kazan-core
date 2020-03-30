using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.ApiResponses
{
    public abstract class ApiResponseBase
    {
        #region Constructors
        protected ApiResponseBase()
        {
            this.Errors = new List<ApiErrorMessage>();
        }

        protected ApiResponseBase(IList<ApiErrorMessage> errors)
        {
            this.Errors = errors;
        }
        #endregion

        #region Properties       
        public IList<ApiErrorMessage> Errors { get; set; }

        //public PaginationMetaData Pagination { get; set; }
        #endregion
    }
}
