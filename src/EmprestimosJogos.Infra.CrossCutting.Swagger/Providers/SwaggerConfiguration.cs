using EmprestimosJogos.Infra.CrossCutting.Swagger.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;

namespace EmprestimosJogos.Infra.CrossCutting.Swagger.Providers
{
    /// <summary>
    /// Referências: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Emprestimos de Jogos API",
                        Version = "v1",
                        Description = "API REST desenvolvida com ASP .NET Core 3.0 para a aplicação <b>Emprestimos de Jogos API</b>."
                    });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Utilização: Escreva 'Bearer {seuToken}'"
                    });

                options.IncludeXmlComments(Path.Combine("wwwroot", "api-docs.xml"));

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

                options.OperationFilter<AddUsuarioIdHeaderParameter>();
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                        .UseSwaggerUI(args =>
                        {
                            /* Rota para acessar a documentação */
                            args.RoutePrefix = "documentation";

                            /* Não alterar: Configuração aplicável tanto para servidor quanto para localhost */
                            args.SwaggerEndpoint("../swagger/v1/swagger.json", "Documentação API v1");

                            args.SwaggerEndpoint("../swagger/v2/swagger.json", "Documentação API v2");

                            args.DocumentTitle = "Emprestimos de Jogos API - Swagger UI";
                        });
        }
    }
}