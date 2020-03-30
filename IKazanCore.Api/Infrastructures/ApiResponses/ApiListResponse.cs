using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.ApiResponses
{
    public class ApiListResponse<T> : ApiResponseBase where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public ApiListResponse()
        {
            Data = new List<T>();
        }
    }
}
