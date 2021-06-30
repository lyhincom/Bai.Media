using Bai.Domain.Settings;
using Bai.Domain.Settings.Getters;
using Bai.General.ApplicationBuilder;
using Bai.General.Environments;
using Bai.General.Environments.Enums;
using Bai.General.JwtToken;
using Bai.General.Swagger;
using Bai.Media.DAL.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bai.Media.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IWebHostEnvironment Environment { get; set; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDomain(Environment.EnvironmentName);
            services.AddApiScopeAuth(DomainUrls.IdentityServer, DomainClientIds.Media);
            services.AddDomainSwaggerGen(DomainClientIds.Media);

            services.AddControllers();

            services.AddDbContext<MediaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql =>
            {
                sql.MigrationsAssembly("Bai.Media.Migrations");
                sql.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MediaDbContext context)
        {
            app.UseDomainDeveloperExceptionPage(env);
            app.UseDomainHsts();
            if (DomainEnvironment.Parse(env.EnvironmentName) == EnvironmentEnum.Local)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bai.Media.Web v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Good World!");
                });
            });

            context.Database.Migrate();
        }
    }
}
