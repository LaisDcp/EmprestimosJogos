using FluentValidation.AspNetCore;
using EmprestimosJogos.Domain.Core.Types;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Providers;
using EmprestimosJogos.Infra.CrossCutting.Identity.Providers;
using EmprestimosJogos.Infra.CrossCutting.IoC;
using EmprestimosJogos.Infra.CrossCutting.Swagger.Providers;
using EmprestimosJogos.Services.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using EmprestimosJogos.Infra.CrossCutting.Auth.Providers;

namespace EmprestimosJogos.Services.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                if (appAssembly != null)
                    builder.AddUserSecrets(appAssembly, optional: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Não alterar a sequência das chamadas no método abaixo.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddWebApi();

            services.AddAutoMapperSetup();
            services.AddCustomIdentityConfiguration();

            services.AddControllers(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version:apiVersion}"));
                options.Conventions.Add(new RouteTokenTransformerConvention(
                                             new SlugifyParameterTransformer()));
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    /*
                        Para passar pelo nosso ModelState.IsValid, senão retorna direto
                        no response, sem passar pelo nosso ExceptionHandlerMiddleware.
                    */
                    options.SuppressModelStateInvalidFilter = true;
                })

                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.UseCamelCasing(true);
                })

                .AddFluentValidation();

            int? _apiVersion = Configuration.GetSection(nameof(ApplicationSettings))?.Get<ApplicationSettings>()?.ApiVersion;

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(_apiVersion ?? 1, 0);
                options.UseApiBehavior = false;
                options.ErrorResponses = new ApiVersionExceptionHandler();
            });

            services.AddCustomJWTConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddHttpContextAccessor();

            services.AddMvc().AddNewtonsoftJson();

            services.AddCors(o => o.AddPolicy("SignalRCorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(h => true)
                    .WithExposedHeaders("X-Total-Count", "X-Has-More");
            }));

            services.AddSignalR(opt => { opt.EnableDetailedErrors = true; });

            services.AddResponseCompression();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Não alterar a sequência das chamadas no método abaixo.
        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseExceptionHandlerMiddleware();
            app.UseSwaggerConfiguration();

            app.UseCors("SignalRCorsPolicy");

            app.UseResponseCompression();

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddLogging(l => l.AddConsole());

            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
