using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Models.Response
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public TokenResponse TokenResponse { get; set; }
    }

    public class TokenResponse
    {
        public string ApiToken { get; set; }
    }
}
