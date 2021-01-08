using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace EmprestimosJogos.Infra.CrossCutting.Swagger.Helpers
{
    public class AddUsuarioIdHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "UsuarioId",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}