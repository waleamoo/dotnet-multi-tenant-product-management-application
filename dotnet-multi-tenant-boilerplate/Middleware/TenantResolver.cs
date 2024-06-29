using dotnet_multi_tenant_boilerplate.Services;

namespace dotnet_multi_tenant_boilerplate.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        // HttpContext Object - contains all the information about the incoming request 
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader); // header with a key-value called "tenant"
            if (string.IsNullOrEmpty(tenantFromHeader) == false)
            {
                // set tenant id in scoped service 
                await currentTenantService.SetTenant(tenantFromHeader);
            }
            await _next(context);
        }
    }
}
