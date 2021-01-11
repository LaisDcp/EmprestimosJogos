using EmprestimosJogos.Domain.Core.Types;
using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Infra.CrossCutting.Auth.Providers;
using EmprestimosJogos.Infra.CrossCutting.ExceptionHandler.Providers;
using EmprestimosJogos.Infra.CrossCutting.Identity.Providers;
using EmprestimosJogos.Infra.CrossCutting.IoC;
using EmprestimosJogos.Infra.Data.Context;
using EmprestimosJogos.Services.Api.Configurations;
using FluentValidation.AspNetCore;
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
using System;
using System.Linq;
using System.Reflection;

namespace EmprestimosJogos.Services.Api
{
    public class StartupTests
    {
        public IConfiguration Configuration { get; }

        public StartupTests(IHostEnvironment env)
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
            services.AddHttpContextAccessor();

            services.AddMvc().AddNewtonsoftJson();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(h => true);
            }));

            services.AddCustomPolicyProviderConfiguration();

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Não alterar a sequência das chamadas no método abaixo.
        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseExceptionHandlerMiddleware();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            /**
             * Seed data for tests
             */
            app.SeedData();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddLogging(l => l.AddConsole());

            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }

    public static class StartupTestsExtension
    {
        private static readonly Guid _perfilId = new Guid("8907D860-CEB1-4345-B798-8757200E90C9");

        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                EmprestimosJogosContext _context = serviceScope.ServiceProvider.GetService<EmprestimosJogosContext>();

                Usuario _usuario = GetUsuario();

                if (!_context.Usuario.Any(wh => wh.Id == _usuario.Id))
                    _context.Usuario.Add(_usuario);

                Jogo _jogo = GetJogo();

                if (!_context.Jogo.Any(wh => wh.Id == _jogo.Id))
                    _context.Jogo.Add(_jogo);

                Amigo _amigo = GetAmigo();

                if (!_context.Amigo.Any(wh => wh.Id == _amigo.Id))
                    _context.Amigo.Add(_amigo);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static Usuario GetUsuario()
        {
            string _email = "adm@email.com.br";

            var _usuario =  new Usuario
            {
                Id = new Guid("3E50E8B0-43AF-414F-B55D-9CA16E4EF5D9"),
                Email = _email,
                Nome = "adm",
                NormalizedEmail = _email.ToUpper(),
                UserName = _email,
                NormalizedUserName = _email.ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAEK3y1PJbnmX9TbwJ61l+ewqcnS6nntFTut3QoU1MT/BUvCNqvsM7j4nwOpFF6Plk/w==", // Senha: 39ag86
                SecurityStamp = "MALPBMVWVII6D6P3JYMP4JZ7MOM27WAO",
                LockoutEnabled = true,
                AccessFailedCount = 0,
                CreatedDate = DateTime.Now,
            };

            _usuario.SetPerfilId(_perfilId);

            return _usuario;
        }

        public static Jogo GetJogo()
        {
            return new Jogo(nome: "Grand Theft Auto: San Andreas",
                            creatorId: GetUsuario().Id,
                            id: new Guid("6434C611-7CA8-499F-B015-D156937023B8"));
        }

        public static Amigo GetAmigo()
        {
            return new Amigo(nome: "Nome Amigo",
                             telefoneCelular: "12123654789",
                             creatorId: GetUsuario().Id,
                             id: new Guid("24E71445-3EA1-4038-A76A-68687A98BD0B"));
        }
    }
}
