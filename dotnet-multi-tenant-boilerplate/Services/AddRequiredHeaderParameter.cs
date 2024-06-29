using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace dotnet_multi_tenant_boilerplate.Services
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "tenant",
                In = ParameterLocation.Header,
                Description = "Name of the Tenant",
                Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString("gamma") },
                Required = true,
            });
        }
    }
}
