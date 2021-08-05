using authenticationJWT.Data;
using authenticationJWT.Services;
using AuthenticationJWT.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text;

namespace authenticationJWT
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthDatabase")));

            services.AddCors();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var statusCode = (int)HttpStatusCode.InternalServerError;

                        var message = new StringBuilder();
                        message.Append($"<h4> Status Code </h4>{statusCode}");
                        message.Append($"<h4> URI </h4>{context.Request.Host.Value}");
                        message.Append($"<h4> URL </h4>{context.Request.Path}");
                        message.Append($"<h4> Message </h4>{exceptionHandlerFeature.Error.Message}");
                        message.Append($"<h4> Stack Trace </h4>{exceptionHandlerFeature.Error.StackTrace}");
                        message.Append($"<h4> Source </h4>{exceptionHandlerFeature.Error.Source}");
                        message.Append($"<br/>");
                        message.Append($"---------------------------------");
                        message.Append($"<h4> Complete error </h4>{exceptionHandlerFeature.Error}");

                        await EmailService.SendAsync(message.ToString());
                    }
                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
