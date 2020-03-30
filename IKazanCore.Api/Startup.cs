using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IKazanCore.Api.Data;
using Microsoft.EntityFrameworkCore;
using IKazanCore.Api.Infrastructures.Utils;
using IKazanCore.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Threading.Tasks;
using IKazanCore.Api.Infrastructures.Enums;
using IKazanCore.Api.Infrastructures.Constants;
using IKazanCore.Api.Infrastructures.ApiResponses;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace IKazanCore.Api
{
    public class Startup
    {
        private const string SQL_CONNECTION = "SqlConnection";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContextPool<KazanCoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(SQL_CONNECTION));
            }, poolSize: 20);

            ConfigJwtAuthentication(services);

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddTransient<IUserService, UserService>();

            //services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigJwtAuthentication(IServiceCollection services)
        {
            var symmetricKey = Convert.FromBase64String(EncryptionUtil.ToBase64Encode("123456abcdefqwer"));

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async context =>
                    {
                        Console.WriteLine("ABCDDDDDDDDDDDDDDDDDDD");
                        var apiErrorMessage = new ApiErrorMessage();
                        var statusCode = HttpStatusCode.Unauthorized;

                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            apiErrorMessage.ErrorCode = (int)ErrorCodeEnum.ACCESS_TOKEN_IS_EXPIRED;
                            apiErrorMessage.ErrorMessage = ErrorMessageConstant.ACCESS_TOKEN_IS_EXPIRED;
                        }
                        else
                        {
                            apiErrorMessage.ErrorCode = (int)ErrorCodeEnum.ACCESS_TOKEN_IS_INVALID;
                            apiErrorMessage.ErrorMessage = ErrorMessageConstant.ACCESS_TOKEN_IS_INVALID;
                            statusCode = (HttpStatusCode.Forbidden);
                        }

                        var apiResponseException = new ApiResponseException()
                        {
                            ErrorMessages = new List<ApiErrorMessage>
                            {
                                apiErrorMessage
                            },
                            StatusCode = statusCode
                        };

                        var apiResponse = new ApiSingleResponse<string>()
                        {
                            Data = null,
                            Errors = apiResponseException.ErrorMessages
                        };

                        context.Response.StatusCode = (int)statusCode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    }
                };
            });
        }
    }
}
