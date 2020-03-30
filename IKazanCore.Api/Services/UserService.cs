using IKazanCore.Api.Data;
using IKazanCore.Api.Infrastructures.Utils;
using IKazanCore.Api.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKazanCore.Api.Services
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync();
    }

    public class UserService : IUserService
    {
        private readonly KazanCoreContext _kazanCoreContext;

        public UserService(KazanCoreContext kazanCoreContext)
        {
            _kazanCoreContext = kazanCoreContext;
        }

        public async Task<LoginResponse> LoginAsync()
        {
            try
            {
                var existingUserEntity = await _kazanCoreContext.Users.
                    FirstOrDefaultAsync(u => u.Email == "test1@gmail.com");
                if (existingUserEntity == null)
                {
                    return null;
                }

                var symmetricKey = Convert.FromBase64String(EncryptionUtil.ToBase64Encode("123456abcdefqwer"));

                var tokenHandler = new JwtSecurityTokenHandler();

                var expiredTime = DateTime.UtcNow.AddSeconds(1800);
                var refreshToken = Guid.NewGuid().ToString();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim("Id", existingUserEntity.Id.ToString()),
                        }
                    ),
                    Issuer = "kazan.com",
                    Expires = expiredTime,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var stoken = tokenHandler.CreateToken(tokenDescriptor);
                var apiToken = tokenHandler.WriteToken(stoken);

                return new LoginResponse()
                {
                    Id = existingUserEntity.Id,
                    Email = existingUserEntity.Email,
                    TokenResponse = new TokenResponse
                    {
                        ApiToken = apiToken
                    }
                };
            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
