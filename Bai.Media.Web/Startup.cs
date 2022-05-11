using System;
using System.Diagnostics;
using Bai.Domain.Settings;
using Bai.Domain.Settings.Getters;
using Bai.General.ApplicationBuilder;
using Bai.General.DAL.Abstractions.Repositories;
using Bai.General.DAL.Repositories;
//using Bai.General.JwtToken;
using Bai.General.Logs;
using Bai.General.Swagger;
using Bai.Media.DAL.Contexts;
using Bai.Media.DAL.Models;
using Bai.Media.Web.Abstractions.Services;
using Bai.Media.Web.Abstractions.Services.PersistenceServices;
using Bai.Media.Web.Models;
using Bai.Media.Web.Services;
using Bai.Media.Web.Services.ConverterServices;
using Bai.Media.Web.Services.MediaPersistenceServices;
using Bai.Media.Web.Services.MediaPersistenceServices.Base;
using Bai.Media.Web.Services.MediaProcessingServices;
using Bai.Media.Web.Services.ValidationServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Serilog;

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
        private static string Message;
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddDomain(Environment?.EnvironmentName);
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

            ////services.AddApiScopeAuth(DomainUrls.IdentityServer, DomainClientIds.Media);
            services.AddDomainSwaggerGen(DomainClientIds.Media);

            services.AddControllers();

            // Database
            services.AddTransient<IDomainRepository<AvatarEntity, Guid>, DomainRepository<AvatarEntity, Guid, MediaDbContext>>();
            services.AddTransient<IDomainRepository<ImageEntity, Guid>, DomainRepository<ImageEntity, Guid, MediaDbContext>>();
            services.AddTransient<IDomainRepository<LogoEntity, Guid>, DomainRepository<LogoEntity, Guid, MediaDbContext>>();

            // Database and FileSystem
            services.AddTransient<IPersistenceService<Avatar, AvatarEntity>, AvatarPersistenceService>();
            services.AddTransient<IPersistenceService<Image, ImageEntity>, ImagePersistenceService>();
            services.AddTransient<IPersistenceService<Logo, LogoEntity>, LogoPersistenceService>();

            services.AddTransient<IFormImageToEntityConverterService<Avatar, AvatarEntity>, FormImageToEntityMapperService<Avatar, AvatarEntity>>();
            services.AddTransient<IFormImageToEntityConverterService<Image, ImageEntity>, FormImageToEntityMapperService<Image, ImageEntity>>();
            services.AddTransient<IFormImageToEntityConverterService<Logo, LogoEntity>, FormImageToEntityMapperService<Logo, LogoEntity>>();

            services.AddTransient<IFormFileValidationService, FormFileValidationService>();
            services.AddTransient<IMagicImageValidationService<Avatar>, AvatarValidationService>();
            services.AddTransient<IMagicImageValidationService<Image>, ImageValidationService>();
            services.AddTransient<IMagicImageValidationService<Logo>, LogoValidationService>();

            services.AddTransient<IFileSystemService, FileSystemService>();

            services.AddTransient<IMediaProcessingService, MediaProcessingService>();

            Debug.WriteLine(Environment.EnvironmentName);
            var connectionString = "Data Source=w11.hoster.by;Initial Catalog=lyhincom_media_qa;User ID=lyhincom_media_qa;Password=!lxb8QEbXpA=KCZhU+h:n5^q8/e/De"; // Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MediaDbContext>(options => options.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly("Bai.Media.Migrations");
                sql.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
            }));

            services.AddLogs();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MediaDbContext context, ILogger logger)
        {
            logger.Warning($"Bai.Media environment {env.EnvironmentName}...");

            //app.UseDomainDeveloperExceptionPage(env);
            //app.UseDomainHsts();
            //if (DomainEnvironment.Parse(env.EnvironmentName) == EnvironmentEnum.Local)
            //{
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bai.Media.Web v1"));
            //}

            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapGet("/", async context =>
                {
                    var message = Message == null ? string.Empty : $" {Message}";
                    await context.Response.WriteAsync($"Good World{message}!");
                });
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath),
                RequestPath = "/Bai.Media.StaticFiles"
            });

            // context.Database.Migrate();

            logger.Information("Good world!");
        }
    }
}
