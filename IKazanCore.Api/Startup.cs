using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IKazanCore.Api.Infrastructures.Options;
using IKazanCore.Api.Data;
using Microsoft.EntityFrameworkCore;

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
            //services.Configure<ConnectionStringOption>(Configuration.GetSection(CONNECTION_STRINGS));

            services.AddDbContextPool<KazanCoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(SQL_CONNECTION));
            }, poolSize: 20);

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
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
