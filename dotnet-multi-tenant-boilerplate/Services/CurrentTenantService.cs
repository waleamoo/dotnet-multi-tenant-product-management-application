using dotnet_multi_tenant_boilerplate.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_multi_tenant_boilerplate.Services
{
    // we will add this service as scoped - because we don't want the value to change throughout the entire lifetime of the request 
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }

        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid");
            }
        }
    }
}
