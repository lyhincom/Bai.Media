using System;
using Bai.Domain.Settings;
using Bai.Domain.Settings.Getters;
using Bai.General.ApplicationBuilder;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.General.DAL.Repositories;
using Bai.General.Environments;
using Bai.General.Environments.Enums;
using Bai.General.JwtToken;
using Bai.General.Swagger;
using Bai.Media.DAL.Contexts;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services;
using Bai.Media.Web.Services.MediaPersistenceServices;
using Bai.Media.Web.Services.MediaPersistenceServices.Base;
using Bai.Media.Web.Services.ValidationServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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

            services.AddTransient<IDomainRepository<AvatarEntity, Guid>, DomainRepository<AvatarEntity, Guid, MediaDbContext>>();
            services.AddTransient<IDomainRepository<ImageEntity, Guid>, DomainRepository<ImageEntity, Guid, MediaDbContext>>();
            services.AddTransient<IDomainRepository<LogoEntity, Guid>, DomainRepository<LogoEntity, Guid, MediaDbContext>>();
            services.AddTransient<IPersistenceService<Avatar>, AvatarPersistenceService>();

            services.AddTransient<IFormFileValidationService, FormFileValidationService>();
            services.AddTransient<IMagicImageValidationService<Avatar, AvatarEntity>, AvatarValidationService>();
            services.AddTransient<IMagicImageValidationService<Image, ImageEntity>, ImageValidationService>();
            services.AddTransient<IMagicImageValidationService<Logo, LogoEntity>, LogoValidationService>();

            services.AddTransient<IFileSystemService, FileSystemService>();
            
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

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath),
                RequestPath = "/Bai.Media.StaticFiles"
            });

            // context.Database.Migrate();
        }
    }
}
